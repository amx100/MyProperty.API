namespace MyProperty.API.Core.Domain.Entities
{
    public class PropertyImage
    {
        public int ImageId { get; set; }
		public int? PropertyId { get; set; }
		public string ImageUrl { get; set; }
        public int Order { get; set; } // Redosled prikazivanja slika

        // Navigaciono svojstvo
        public Property? Property { get; set; }
    }
}
