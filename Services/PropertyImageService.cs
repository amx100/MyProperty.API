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
            if (image == null || image.PropertyId != propertyId)
            {
                throw new ArgumentException("Image not found or does not belong to the property.");
            }

            return image.Adapt<PropertyImageDto>();
        }

        public async Task<GeneralResponseDto> Create(int propertyId, PropertyImageCreateDto imageDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var image = imageDto.Adapt<PropertyImage>();
                image.PropertyId = propertyId;

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
                    IsSuccess = true,
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

                return new GeneralResponseDto { IsSuccess = true, Message = "Image updated successfully!" };
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

        public async Task<GeneralResponseDto> Update(int propertyId, IEnumerable<PropertyImageUpdateDto> imageDtos, CancellationToken cancellationToken = default)
        {
            try
            {
                var imagesToUpdate = imageDtos.Select(dto => dto.Adapt<PropertyImage>()).ToList();
                var existingImages = await repositoryManager.PropertyImageRepository.GetImagesByPropertyId(propertyId, cancellationToken);

                if (existingImages == null || !existingImages.Any())
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "No images found for the specified property."
                    };
                }

                foreach (var image in imagesToUpdate)
                {
                    var existingImage = existingImages.FirstOrDefault(img => img.ImageId == image.ImageId);
                    if (existingImage != null)
                    {
                        image.Adapt(existingImage);
                        repositoryManager.PropertyImageRepository.Update(existingImage);
                    }
                }

                var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                return new GeneralResponseDto
                {
                    IsSuccess = true,
                    Message = $"{rowsAffected} images updated successfully!"
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

        public async Task<GeneralResponseDto> Delete(int imageId, CancellationToken cancellationToken = default)
        {
            var image = await repositoryManager.PropertyImageRepository.GetById(imageId, cancellationToken);

            if (image == null)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Image not found."
                };
            }

            repositoryManager.PropertyImageRepository.DeleteImage(image, cancellationToken);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new GeneralResponseDto
            {
                IsSuccess = true,
                Message = "Image deleted successfully."
            };
        }
    }
}
