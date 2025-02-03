using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;
using Persistence;


public class PropertyRepository(DataContext dataContext) : RepositoryBase<Property>(dataContext), IPropertyRepository
{
	public void CreateProperty(Property property, CancellationToken cancellationToken = default) => Create(property);

	public void DeleteProperty(Property property, CancellationToken cancellationToken = default) => Delete(property); 

	public void UpdateProperty(Property property, CancellationToken cancellationToken = default) => Update(property);

	public async Task<IEnumerable<Property>> GetAllProperties(CancellationToken cancellationToken = default)
	{
		return await FindAll(trackChanges: false)
			.Include(p => p.Images)
			.ToListAsync(cancellationToken);
	}

	public async Task<Property> GetById(int propertyId, CancellationToken cancellationToken)
	{
		return await FindByCondition(p => p.PropertyId == propertyId)
			.Include(p => p.Images)
			.Include(p => p.Reservations)  // Include reservations as well
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IEnumerable<Property>> GetPropertiesByStatus(string status, CancellationToken cancellationToken = default)
	{
		return await FindByCondition(p => p.Status == status).ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Property>> GetPropertiesByType(string propertyType, CancellationToken cancellationToken = default)
	{
		return await FindByCondition(p => p.PropertyType == propertyType).ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Property>> GetPropertiesInPriceRange(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default)
	{
		return await FindByCondition(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync(cancellationToken);
	}
}
