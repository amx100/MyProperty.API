namespace Contract;

public class TransactionDto
{
	public int Id { get; set; }
	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }
	public string BuyerAccountId { get; set; } = string.Empty;
	public string OwnerAccountId { get; set; } = string.Empty;
	public int PropertyId { get; set; }
}

public class TransactionCreateDto
{
	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }
	public string BuyerAccountId { get; set; } = string.Empty;
	public string OwnerAccountId { get; set; } = string.Empty;
	public int PropertyId { get; set; }
}

public class TransactionUpdateDto
{
	public int Id { get; set; }
	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }
	public string BuyerAccountId { get; set; } = string.Empty;
	public string OwnerAccountId { get; set; } = string.Empty;
	public int PropertyId { get; set; }
}
