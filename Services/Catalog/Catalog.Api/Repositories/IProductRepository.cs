using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
