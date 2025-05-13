using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class SelectionSort : BaseSortAlgorithm
    {
        public override string Name => "Selection Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);

            for (int i = 0; i < result.Count - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (ShouldSwap(result[minIdx], result[j], sortBy))
                    {
                        minIdx = j;
                    }
                }

                if (minIdx != i)
                {
                    Swap(result, i, minIdx);
                }
            }

            return ApplySortDirection(result, sortDirection);
        }
    }
} 