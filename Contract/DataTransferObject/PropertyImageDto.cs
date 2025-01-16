namespace Contract;

public class PropertyImageDto
{
	public int Id { get; set; }
	public string ImageUrl { get; set; } = string.Empty;
	
}

public class PropertyImageCreateDto
{
	public string ImageUrl { get; set; } = string.Empty;
	public int PropertyId { get; set; } //removed comment line for ui  // Nullable for unlinked images
}

public class PropertyImageUpdateDto
{
	public int Id { get; set; }
	public string ImageUrl { get; set; } = string.Empty;
	public int PropertyId { get; set; }
}
