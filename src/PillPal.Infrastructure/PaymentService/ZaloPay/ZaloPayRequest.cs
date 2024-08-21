using PillPal.Infrastructure.Utils;

namespace PillPal.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayRequest
{
    public string? AppId { get; set; }
    public string? AppUser { get; set; }
    public long AppTime { get; set; }
    public long Amount { get; set; }
    public string? AppTransId { get; set; }
    public string? ReturnUrl { get; set; }
    public string? EmbedData { get; set; }
    public string? Mac { get; set; }
    public string? BankCode { get; set; }
    public string? Description { get; set; }
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

    public void MakeSignature(string key)
    {
        var data = AppId + "|" + AppTransId + "|" + AppUser + "|" + Amount + "|" + AppTime + "|" + "|";

        Mac = HashHelper.HmacSHA256(data, key);
    }

    public Dictionary<string, string> GetContent()
    {
        var keyValuePairs = new Dictionary<string, string>
        {
            { "appid", AppId! },
            { "appuser", AppUser! },
            { "apptime", AppTime.ToString() },
            { "amount", Amount.ToString() },
            { "apptransid", AppTransId! },
            { "description", Description! },
            { "bankcode", "zalopayapp" },
            { "mac", Mac! }
        };

        return keyValuePairs;
    }

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public (string paymentUrl, string zpTransToken) GetLink(string paymentUrl)
    {
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(GetContent());
        var response = client.PostAsync(paymentUrl, content).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonSerializer.Deserialize<ZaloPayResponse>(responseContent, _jsonSerializerOptions);

            return (responseData!.OrderUrl!, responseData.ZpTransToken!);
        }
        else
        {
            return (response.ReasonPhrase ?? string.Empty, "");
        }
    }
}
