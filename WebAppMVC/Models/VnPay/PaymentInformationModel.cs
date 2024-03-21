namespace WebAppMVC.Models.VnPay
{
    public class PaymentInformationModel
    {
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
    }
}
