using PillPal.Infrastructure.Utils;
using System.Net;

namespace PillPal.Infrastructure.PaymentService.VnPay;

public class VnPayResponse
{
    public SortedList<string, string> responseData = new SortedList<string, string>(new VnPayCompare());
    public string? Vnp_TmnCode { get; set; }
    public string? Vnp_BankCode { get; set; }
    public string? Vnp_BankTranNo { get; set; }
    public string? Vnp_CardType { get; set; }
    public string? Vnp_OrderInfo { get; set; }
    public string? Vnp_TransactionNo { get; set; }
    public string? Vnp_TransactionStatus { get; set; }
    public string? Vnp_TxnRef { get; set; }
    public string? Vnp_SecureHashType { get; set; }
    public string? Vnp_SecureHash { get; set; }
    public int? Vnp_Amount { get; set; }
    public string? Vnp_ResponseCode { get; set; }
    public string? Vnp_PayDate { get; set;}

    public bool IsValidSignature(string secretKey)
    {
        MakeResponseData();
        StringBuilder data = new();
        foreach (KeyValuePair<string, string> kv in responseData)
        {
            if (!string.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }
        string checkSum = HashHelper.HmacSHA512(secretKey, 
            data.ToString().Remove(data.Length - 1, 1));
        return checkSum.Equals(Vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
    }

    public void MakeResponseData()
    {
        if (Vnp_Amount != null)
            responseData.Add("vnp_Amount", Vnp_Amount.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_TmnCode))
            responseData.Add("vnp_TmnCode", Vnp_TmnCode.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_BankCode))
            responseData.Add("vnp_BankCode", Vnp_BankCode.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_BankTranNo))
            responseData.Add("vnp_BankTranNo", Vnp_BankTranNo.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_CardType))
            responseData.Add("vnp_CardType", Vnp_CardType.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_OrderInfo))
            responseData.Add("vnp_OrderInfo", Vnp_OrderInfo.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_TransactionNo))
            responseData.Add("vnp_TransactionNo", Vnp_TransactionNo.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_TransactionStatus))
            responseData.Add("vnp_TransactionStatus", Vnp_TransactionStatus.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_TxnRef))
            responseData.Add("vnp_TxnRef", Vnp_TxnRef.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_PayDate))
            responseData.Add("vnp_PayDate", Vnp_PayDate.ToString() ?? string.Empty);
        if (!string.IsNullOrEmpty(Vnp_ResponseCode))
            responseData.Add("vnp_ResponseCode", Vnp_ResponseCode ?? string.Empty);
    }
}
