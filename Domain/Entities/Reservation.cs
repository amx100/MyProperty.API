namespace MyProperty.API.Core.Domain.Entities
{
	public class Reservation
	{
		public int ReservationId { get; set; }
		public string AccountId { get; set; } = string.Empty;
		public int PropertyId { get; set; }
		public DateTime ReservationDate { get; set; }
		public string Status { get; set; } // "Pending", "Confirmed", "Declined"

	
		public Account User { get; set; }
		public Property Property { get; set; }
	}
}
