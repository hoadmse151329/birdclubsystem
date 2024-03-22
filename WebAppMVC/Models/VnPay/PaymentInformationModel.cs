namespace WebAppMVC.Models.VnPay
{
    public class PaymentInformationModel
    {
        public string UserId { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
    }
}
