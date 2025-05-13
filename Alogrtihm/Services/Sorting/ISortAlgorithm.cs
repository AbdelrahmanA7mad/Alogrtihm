using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public interface ISortAlgorithm
    {
        List<Product> Sort(List<Product> products, string sortBy, string sortDirection);
        string Name { get; }
    }
} 