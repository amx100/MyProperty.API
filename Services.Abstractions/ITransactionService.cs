using Contract;

namespace Services.Abstractions;

public interface ITransactionService
{
	Task<IEnumerable<TransactionDto>> GetAll(CancellationToken cancellationToken = default);

	Task<TransactionDto> GetById(int transactionId, CancellationToken cancellationToken = default);

	Task<GeneralResponseDto> Create(TransactionCreateDto transactionDto, CancellationToken cancellationToken = default);

	Task<GeneralResponseDto> Update(int transactionId, TransactionUpdateDto transactionDto, CancellationToken cancellationToken = default);

	Task Delete(int transactionId, CancellationToken cancellationToken = default);
}
