using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace Services
{

	//public class ReservationService(IRepositoryManager repositoryManager) : IReservationService
	public class PropertyService(IRepositoryManager repositoryManager) : IPropertyService
	{


		public async Task<GeneralResponseDto> Create(PropertyCreateDto propertyDto, CancellationToken cancellationToken)
		{
			try
			{
				var property = propertyDto.Adapt<Property>();
				// Explicitly set the initial status to "Available"
				property.Status = "Available";
				
				repositoryManager.PropertyRepository.CreateProperty(property);
				var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				if (rowsAffected != 1)
				{
					return new GeneralResponseDto { IsSuccess = false, Message = "Error while creating the property." };
				}

				return new GeneralResponseDto { IsSuccess = true, Message = "Property created successfully!" };
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


		public async Task<IEnumerable<PropertyDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var properties = await repositoryManager.PropertyRepository.GetAllProperties(cancellationToken);

			var propertyDtos = properties.Select(p => new PropertyDto
			{
				PropertyId = p.PropertyId, // Ensure you are mapping the PropertyId
				Title = p.Title,
				Description = p.Description,
				Address = p.Address,
				Price = p.Price,
				PropertyType = p.PropertyType,
				Status = p.Status,
				Area = p.Area,
				Images = p.Images?.Select(img => new PropertyImageDto
				{
					Id = img.ImageId,
					ImageUrl = img.ImageUrl,
					//opertyId = (int)img.PropertyId
				}).ToList() ?? new List<PropertyImageDto>()
			});

			return propertyDtos;
		}


		public async Task<GeneralResponseDto> GetById(int propertyId, CancellationToken cancellationToken = default)
		{
			var property = await repositoryManager.PropertyRepository.GetById(propertyId, cancellationToken);

			if (property == null)
			{
				return new GeneralResponseDto
				{
					IsSuccess = false,
					Message = $"Property with ID {propertyId} not found."
					// PropertyId is omitted here
				};
			}

			var propertyDto = new PropertyDto
			{
				PropertyId = property.PropertyId,
				Title = property.Title,
				Description = property.Description,
				Address = property.Address,
				Price = property.Price,
				PropertyType = property.PropertyType,
				Status = property.Status,
				Area = property.Area,
				Images = property.Images?.Select(img => new PropertyImageDto
				{
					Id = img.ImageId,
					ImageUrl = img.ImageUrl
				}).ToList() ?? new List<PropertyImageDto>()
			};

			return new GeneralResponseDto
			{
				Data = propertyDto,
				IsSuccess = true,
				Message = "Property found successfully.",
			};
		}

        public async Task<GeneralResponseDto> Update(int propertyId, PropertyUpdateDto propertyDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var property = await repositoryManager.PropertyRepository.GetById(propertyId, cancellationToken);
                if (property == null)
                {
                    return new GeneralResponseDto { IsSuccess = false, Message = "Property not found." };
                }

                // Update property fields
                property.Title = propertyDto.Title;
                property.Description = propertyDto.Description;
                property.Address = propertyDto.Address;
                property.Price = propertyDto.Price;
                property.PropertyType = propertyDto.PropertyType;
                property.Area = propertyDto.Area;

                // Only update status if there are no confirmed reservations
                if (propertyDto.Status != property.Status)
                {
                    var hasConfirmedReservations = await repositoryManager.PropertyRepository.HasActiveReservations(propertyId, cancellationToken);
                    if (!hasConfirmedReservations)
                    {
                        property.Status = propertyDto.Status;
                    }
                    else
                    {
                        return new GeneralResponseDto 
                        { 
                            IsSuccess = false, 
                            Message = "Cannot update property status because it has active reservations." 
                        };
                    }
                }

                repositoryManager.PropertyRepository.UpdateProperty(property);
                var result = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new GeneralResponseDto 
                    { 
                        IsSuccess = false, 
                        Message = "No changes were made to the property." 
                    };
                }

                return new GeneralResponseDto 
                { 
                    IsSuccess = true, 
                    Message = "Property updated successfully!" 
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error while updating the property: {ex.Message}"
                };
            }
        }



        public async Task<GeneralResponseDto> Delete(int propertyId, CancellationToken cancellationToken = default)
        {
            try
            {
                var property = await repositoryManager.PropertyRepository.GetById(propertyId, cancellationToken);
                if (property == null)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Property not found."
                    };
                }

                // Enable change tracking for the delete operation
                repositoryManager.PropertyRepository.Delete(property);
                var result = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                if (result > 0)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = true,
                        Message = "Property and all related data successfully deleted."
                    };
                }
                else
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "No changes were made to the database."
                    };
                }
            }
            catch (Exception ex)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error deleting property: {ex.Message}"
                };
            }
        }



    }
}
