using Contract;

namespace Services.Abstractions
{
	public interface IPropertyService
	{
		Task<IEnumerable<PropertyDto>> GetAll(CancellationToken cancellationToken = default);
		Task<GeneralResponseDto> GetById(int propertyId, CancellationToken cancellationToken = default);

		Task<GeneralResponseDto> Create(PropertyCreateDto propertyDto, CancellationToken cancellationToken = default);
		Task<GeneralResponseDto> Update(int propertyId, PropertyUpdateDto propertyDto, CancellationToken cancellationToken = default);
		Task<GeneralResponseDto> Delete(int propertyId, CancellationToken cancellationToken = default); 
	}
}
