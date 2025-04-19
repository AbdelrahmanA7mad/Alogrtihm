using Algorithm.Models;
using Alogrtihm.Services;
using System.Text.Json;

namespace Algorithm.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "products.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            else
            {
                _products = new List<Product>(); // Or throw an exception/log warning
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public List<Product> FilterProducts(List<Product> products, string category, string brand, decimal? minPrice,
                                      decimal? maxPrice, double? minRating, List<string> requiredFeatures, bool? inStock)
        {
            return products.Where(p =>
                (string.IsNullOrEmpty(category) || p.Category == category) &&
                (string.IsNullOrEmpty(brand) || p.Brand == brand) &&
                (!minPrice.HasValue || p.Price >= minPrice.Value) &&
                (!maxPrice.HasValue || p.Price <= maxPrice.Value) &&
                (!minRating.HasValue || p.Rating >= minRating.Value) &&
                (!inStock.HasValue || p.InStock == inStock.Value) &&
                (requiredFeatures == null || requiredFeatures.Count == 0 ||
                 requiredFeatures.All(f => p.Features.Contains(f, StringComparer.OrdinalIgnoreCase)))
            ).ToList();
        }
    }
}
