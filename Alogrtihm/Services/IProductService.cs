using Algorithm.Models;

namespace Alogrtihm.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> FilterProducts(List<Product> products, string category, string brand, decimal? minPrice,
                                  decimal? maxPrice, double? minRating, List<string> requiredFeatures, bool? inStock);
    }
}
