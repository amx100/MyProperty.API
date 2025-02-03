using System.Transactions;

namespace MyProperty.API.Core.Domain.Entities
{
    public class Property
    {
		public int PropertyId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public decimal Price { get; set; }
		public string PropertyType { get; set; } // "House", "Apartment", "Office"
		public string Status { get; set; } // "Available", "Reserved", "Sold"
		public double Area { get; set; }

		// Navigational property to hold related images
		public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
