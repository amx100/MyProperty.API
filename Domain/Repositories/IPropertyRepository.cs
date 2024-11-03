using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace MyProperty.API.Core.Domain.Repositories
{
    public interface IPropertyRepository : IRepositoryBase<Property>
    {
        Task<IEnumerable<Property>> GetAllProperties(CancellationToken cancellationToken = default);
        Task<Property> GetById(int propertyId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Property>> GetPropertiesByStatus(string status, CancellationToken cancellationToken = default);
        Task<IEnumerable<Property>> GetPropertiesByType(string propertyType, CancellationToken cancellationToken = default);
        Task<IEnumerable<Property>> GetPropertiesInPriceRange(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default);

        void CreateProperty(Property property, CancellationToken cancellationToken = default);
        void DeleteProperty(Property property, CancellationToken cancellationToken = default);
        void UpdateProperty(Property property, CancellationToken cancellationToken = default);
    }
}
