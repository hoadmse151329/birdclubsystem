namespace WebAppMVC.Models.VnPay
{
    public class PaymentResponseModel
    {
        public string TransactionId { get; set; }
        public string OrderDescription { get; set; }
        public string DocNo { get; set; }
        public string PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
