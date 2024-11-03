using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace Services
{
	public class PropertyImageService : IPropertyImageService
	{
		private readonly IRepositoryManager repositoryManager;

		public PropertyImageService(IRepositoryManager repositoryManager)
		{
			this.repositoryManager = repositoryManager;
		}

		public async Task<IEnumerable<PropertyImageDto>> GetAll(int propertyId, CancellationToken cancellationToken = default)
		{
			var images = await repositoryManager.PropertyImageRepository.GetImagesByPropertyId(propertyId, cancellationToken);
			return images.Select(img => new PropertyImageDto
			{
				Id = img.ImageId,
				ImageUrl = img.ImageUrl,
				
			}).ToList();
		}


		public async Task<PropertyImageDto> GetById(int propertyId, int imageId, CancellationToken cancellationToken = default)
		{
			var image = await repositoryManager.PropertyImageRepository.GetById(imageId, cancellationToken);
			return image.Adapt<PropertyImageDto>();
		}

		public async Task<GeneralResponseDto> Create(int propertyId, PropertyImageCreateDto imageDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var image = imageDto.Adapt<PropertyImage>();
				image.PropertyId = propertyId;  // Povezivanje slike sa svojstvom

				repositoryManager.PropertyImageRepository.Create(image);
				var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				if (rowsAffected != 1)
				{
					return new GeneralResponseDto
					{
						IsSuccess = false,
						Message = "Error while creating the image."
					};
				}

				return new GeneralResponseDto
				{
					Message = "Image created successfully!",
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

		public async Task<GeneralResponseDto> Update(int propertyId, int imageId, PropertyImageUpdateDto imageDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var existingImage = await repositoryManager.PropertyImageRepository.GetById(imageId, cancellationToken);
				if (existingImage == null || existingImage.PropertyId != propertyId)
				{
					return new GeneralResponseDto { IsSuccess = false, Message = "Image not found or does not belong to the property." };
				}

				imageDto.Adapt(existingImage);
				repositoryManager.PropertyImageRepository.Update(existingImage);
				var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				if (rowsAffected != 1)
				{
					return new GeneralResponseDto { IsSuccess = false, Message = "Error while updating the image." };
				}

				return new GeneralResponseDto { Message = "Image updated successfully!" };
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

		public async Task Delete(int imageId, CancellationToken cancellationToken = default)
		{
			var image = await repositoryManager.PropertyImageRepository.GetById(imageId, cancellationToken);

			if (image == null)
			{
				throw new Exception("Image not found");
			}

			// Call the Delete method without needing propertyId
			repositoryManager.PropertyImageRepository.DeleteImage(image, cancellationToken);
			await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}



	}
}
