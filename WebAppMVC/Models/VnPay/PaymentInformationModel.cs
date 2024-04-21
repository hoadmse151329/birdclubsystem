using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models.VnPay
{
    public class PaymentInformationModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Transaction Type is Required")]
        public string TransactionType { get; set; }
        [DataType(DataType.Currency)]
        [Range(1,int.MaxValue, ErrorMessage = "Payment Amount must be more than 0")]
        [Required(ErrorMessage = "Payment Amount is Required")]
        public decimal PayAmount { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name is invalid")]
        public string Fullname { get; set; }
    }
}
