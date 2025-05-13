using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class InsertionSort : BaseSortAlgorithm
    {
        public override string Name => "Insertion Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);

            for (int i = 1; i < result.Count; i++)
            {
                var key = result[i];
                int j = i - 1;

                while (j >= 0 && ShouldSwap(result[j], key, sortBy))
                {
                    result[j + 1] = result[j];
                    j--;
                }
                result[j + 1] = key;
            }

            return ApplySortDirection(result, sortDirection);
        }
    }
} 