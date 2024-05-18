namespace WebAppMVC.Models.Momo
{
    public class MomoCreatePaymentResponseModel
    {
        public string RequestId { get; set; }
        public int ErrorCode { get; set; }
        public int TransactionId { get; set; }
        public string Message { get; set; }
        public string LocalMessage { get; set; }
        public string RequestType { get; set; }
        public string PayUrl { get; set; }
        public string Signature { get; set; }
        public string QrCodeUrl { get; set; }
        public string DeepLink { get; set; }
        public string DeepLinkWebInApp { get; set; }
    }
}
