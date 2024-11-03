namespace MyProperty.API.Core.Domain.Entities
{
	public class Transaction
	{
		public int TransactionId { get; set; }
		public int PropertyId { get; set; }
		public string BuyerId { get; set; } = string.Empty; // promenjeno na string
		public string OwnerId { get; set; } = string.Empty; // promenjeno na string
		public DateTime TransactionDate { get; set; }
		public decimal Amount { get; set; }

		// Navigaciona svojstva
		public Property Property { get; set; }
		public Account Buyer { get; set; }
		public Account Owner { get; set; } 
	}
}
