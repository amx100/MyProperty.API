using Domain.Entities;

namespace MyProperty.API.Core.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int PropertyId { get; set; }
        public string AccountId { get; set; } = string.Empty;

        // Navigation properties
        public virtual Property Property { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}