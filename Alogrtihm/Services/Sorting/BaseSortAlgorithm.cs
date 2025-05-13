using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public abstract class BaseSortAlgorithm : ISortAlgorithm
    {
        public abstract List<Product> Sort(List<Product> products, string sortBy, string sortDirection);
        public abstract string Name { get; }

        protected bool ShouldSwap(Product a, Product b, string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "price" => a.Price <= b.Price,
                "rating" => a.Rating <= b.Rating,
                "name" => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase) <= 0,
                _ => false
            };
        }

        protected void Swap(List<Product> products, int i, int j)
        {
            var temp = products[i];
            products[i] = products[j];
            products[j] = temp;
        }

        protected List<Product> ApplySortDirection(List<Product> products, string sortDirection)
        {
            if (sortDirection.ToLower() == "desc")
            {
                products.Reverse();
            }
            return products;
        }
    }
} 