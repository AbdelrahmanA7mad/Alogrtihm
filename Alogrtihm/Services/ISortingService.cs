using Algorithm.Models;

namespace Alogrtihm.Services
{
    public interface ISortingService
    {
        List<Product> SortProducts(List<Product> products, string sortBy, string sortDirection , string sortAlgorithm);
        List<Product> SortByPrice(List<Product> products, string sortDirection);
        List<Product> SortByRating(List<Product> products, string sortDirection);
        List<Product> SortByName(List<Product> products, string sortDirection);
    }
}
