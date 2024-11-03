using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;
using Services.Abstractions;

namespace Services
{
	public class TransactionService(IRepositoryManager repositoryManager) : ITransactionService
	{
		public async Task<IEnumerable<TransactionDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var transactions = await repositoryManager.TransactionRepository.GetAllTransactions(cancellationToken);
			return transactions.Adapt<IEnumerable<TransactionDto>>();
		}

		public async Task<TransactionDto> GetById(int transactionId, CancellationToken cancellationToken = default)
		{
			var transaction = await repositoryManager.TransactionRepository.GetById(transactionId, cancellationToken);
			return transaction.Adapt<TransactionDto>();
		}

		public async Task<GeneralResponseDto> Create(TransactionCreateDto transactionDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var transaction = transactionDto.Adapt<Transaction>();
				repositoryManager.TransactionRepository.Create(transaction);
				var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				return new GeneralResponseDto
				{
					IsSuccess = rowsAffected == 1,
					Message = rowsAffected == 1 ? "Transaction created successfully!" : "Error creating transaction."
				};
			}
			catch (Exception ex)
			{
				return new GeneralResponseDto
				{
					IsSuccess = false,
					Message = ex.Message
				};
			}
		}

		public async Task<GeneralResponseDto> Update(int transactionId, TransactionUpdateDto transactionDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var existingTransaction = await repositoryManager.TransactionRepository.GetById(transactionId, cancellationToken);
				if (existingTransaction == null)
				{
					return new GeneralResponseDto { IsSuccess = false, Message = "Transaction not found." };
				}

				transactionDto.Adapt(existingTransaction);
				repositoryManager.TransactionRepository.Update(existingTransaction);
				var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				return new GeneralResponseDto
				{
					IsSuccess = rowsAffected == 1,
					Message = rowsAffected == 1 ? "Transaction updated successfully!" : "Error updating transaction."
				};
			}
			catch (Exception ex)
			{
				return new GeneralResponseDto
				{
					IsSuccess = false,
					Message = ex.Message
				};
			}
		}



		public async Task Delete(int transactionId, CancellationToken cancellationToken = default)
		{
			var transaction = await repositoryManager.TransactionRepository.GetById(transactionId, cancellationToken);
			if (transaction != null)
			{
				repositoryManager.TransactionRepository.Delete(transaction);
				await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
