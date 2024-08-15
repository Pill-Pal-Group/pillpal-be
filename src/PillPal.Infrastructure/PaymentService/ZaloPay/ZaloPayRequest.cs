using PillPal.Infrastructure.Utils;

namespace PillPal.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayRequest
{
    public ZaloPayRequest(string appId, string appUser, long appTime,
        long amount, string appTransId, string bankCode, string description)
    {
        AppId = appId;
        AppUser = appUser;
        AppTime = appTime;
        Amount = amount;
        AppTransId = appTransId;
        BankCode = bankCode;
        Description = description;
    }
    public string AppId { get; set; }
    public string AppUser { get; set; } = string.Empty;
    public long AppTime { get; set; }
    public long Amount { get; set; }
    public string AppTransId { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
    public string EmbedData { get; set; } = string.Empty;
    public string Mac { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public void MakeSignature(string key)
    {
        var data = AppId + "|" + AppTransId + "|" + AppUser + "|" + Amount + "|"
        + AppTime + "|" + "|";

        Mac = HashHelper.HmacSHA256(data, key);
    }

    public Dictionary<string, string> GetContent()
    {
        var keyValuePairs = new Dictionary<string, string>
        {
            { "appid", AppId },
            { "appuser", AppUser },
            { "apptime", AppTime.ToString() },
            { "amount", Amount.ToString() },
            { "apptransid", AppTransId },
            { "description", Description },
            { "bankcode", "zalopayapp" },
            { "mac", Mac }
        };

        return keyValuePairs;
    }

    public (bool, string, string) GetLink(string paymentUrl)
    {
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(GetContent());
        var response = client.PostAsync(paymentUrl, content).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonSerializer
                .Deserialize<ZaloPayResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (responseData!.returnCode == 1)
            {
                return (true, responseData.orderUrl!, responseData.zpTransToken!);
            }
            else
            {
                return (false, responseData.returnMessage!, "");
            }

        }
        else
        {
            return (false, response.ReasonPhrase ?? string.Empty, "");
        }
    }
}
