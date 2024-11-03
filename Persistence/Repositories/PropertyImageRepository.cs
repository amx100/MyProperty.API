using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;
using Persistence;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories;

public class PropertyImageRepository(DataContext dataContext) : RepositoryBase<PropertyImage>(dataContext), IPropertyImageRepository
{
	
    private readonly DataContext _context;

	public async Task<IEnumerable<PropertyImage>> GetImagesByIds(IEnumerable<int> imageIds, CancellationToken cancellationToken = default)
	{
		return await _context.PropertiesImages
			.Where(image => imageIds.Contains(image.ImageId))
			.ToListAsync(cancellationToken);
	}
	public void CreateImage(PropertyImage image, CancellationToken cancellationToken = default) => Create(image);

    public void DeleteImage(PropertyImage image, CancellationToken cancellationToken = default) => Delete(image);

    public void UpdateImage(PropertyImage image, CancellationToken cancellationToken = default) => Update(image);

    public async Task<IEnumerable<PropertyImage>> GetAllImages(CancellationToken cancellationToken = default)
    {
        return await FindAll().ToListAsync(cancellationToken);
    }

    public async Task<PropertyImage> GetById(int imageId, CancellationToken cancellationToken = default)
    {
        return await FindByCondition(img => img.ImageId == imageId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyImage>> GetImagesByPropertyId(int propertyId, CancellationToken cancellationToken = default)
    {
		return await FindByCondition(img => img.PropertyId == propertyId)
		.ToListAsync(cancellationToken);
	}
}