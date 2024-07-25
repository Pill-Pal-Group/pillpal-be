using PillPal.Infrastructure.Utils;
using System.Net;

namespace PillPal.Infrastructure.PaymentService.VnPay;

public class VnPayRequest
{
    public SortedList<string, string> requestData = new(new VnPayCompare());
    public VnPayRequest(
        string version, 
        string tmnCode, 
        string ipAddress,
        decimal amount, 
        string orderInfo,
        string returnUrl, 
        string txnRef)
    {
        Vnp_IpAddr = ipAddress;
        Vnp_Version = version;
        Vnp_TmnCode = tmnCode;
        Vnp_Amount = (int)amount * 100;
        Vnp_OrderInfo = orderInfo;
        Vnp_ReturnUrl = returnUrl;
        Vnp_TxnRef = txnRef;
    }

    public string GetLink(string baseUrl, string secretKey)
    {
        MakeRequestData();
        
        StringBuilder data = new();
        foreach(KeyValuePair<string, string> kv in requestData)
        {
            if(!string.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }

        string result = baseUrl + "?" + data.ToString();
        var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
        return result += "vnp_SecureHash=" + secureHash;
    }

    private void MakeRequestData()
    {
        requestData.Add("vnp_Command", "pay");
        requestData.Add("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        requestData.Add("vnp_CurrCode", "VND");
        requestData.Add("vnp_Locale", "vn");
        requestData.Add("vnp_OrderType", "billpayment");
        requestData.Add("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

        if (Vnp_Amount != null)
            requestData.Add("vnp_Amount", Vnp_Amount.ToString() ?? string.Empty);
        if (Vnp_BankCode != null)
            requestData.Add("vnp_BankCode", Vnp_BankCode);
        if (Vnp_IpAddr != null)
            requestData.Add("vnp_IpAddr", Vnp_IpAddr);
        if (Vnp_OrderInfo != null)
            requestData.Add("vnp_OrderInfo", Vnp_OrderInfo);
        if (Vnp_ReturnUrl != null)
            requestData.Add("vnp_ReturnUrl", Vnp_ReturnUrl);
        if (Vnp_TmnCode != null)
            requestData.Add("vnp_TmnCode", Vnp_TmnCode);
        if (Vnp_TxnRef != null)
            requestData.Add("vnp_TxnRef", Vnp_TxnRef);
        if (Vnp_Version != null)
            requestData.Add("vnp_Version", Vnp_Version);
    }
    public decimal? Vnp_Amount { get; set; }
    public string? Vnp_BankCode { get; set; }
    public string? Vnp_IpAddr { get; set; }
    public string? Vnp_OrderInfo { get; set; }
    public string? Vnp_ReturnUrl { get; set; }
    public string? Vnp_TmnCode { get; set; }
    public string? Vnp_TxnRef { get; set; }
    public string? Vnp_Version { get; set; }
    public string? Vnp_SecureHash { get; set; }
}
