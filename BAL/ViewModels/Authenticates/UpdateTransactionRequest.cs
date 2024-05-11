using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Authenticates
{
	public class UpdateTransactionRequest
	{
		public string MemberId { get; set;}
		public int? TransactionId { get; set;}
	}
}
