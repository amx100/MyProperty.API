using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace MyProperty.API.Core.Domain.Repositories
{
    public interface IPropertyImageRepository : IRepositoryBase<PropertyImage>
    {
		Task<IEnumerable<PropertyImage>> GetImagesByIds(IEnumerable<int> imageIds, CancellationToken cancellationToken = default);
		Task<PropertyImage> GetById(int imageId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PropertyImage>> GetImagesByPropertyId(int propertyId, CancellationToken cancellationToken = default);

        void CreateImage(PropertyImage image, CancellationToken cancellationToken = default);
        void DeleteImage(PropertyImage image, CancellationToken cancellationToken = default);
        void UpdateImage(PropertyImage image, CancellationToken cancellationToken = default);
    }
}
