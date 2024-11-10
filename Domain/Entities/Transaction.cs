using Domain.Entities;
using MyProperty.API.Core.Domain.Entities;

public class Transaction
{
	public int Id { get; set; }
	public string BuyerId { get; set; }
	public Account Buyer { get; set; }

	public string OwnerId { get; set; }
	public Account Owner { get; set; }

	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }
	public string ReservationId { get; set; }
	public Reservation Reservation { get; set; }

	public string Status { get; set; } // e.g., "Completed", "Failed"
}
