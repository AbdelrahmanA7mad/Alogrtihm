using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class ShellSort : BaseSortAlgorithm
    {
        public override string Name => "Shell Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);
            int n = result.Count;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {

                for (int i = gap; i < n; i++)
                {
                    var temp = result[i];

                  
                    int j;
                    for (j = i; j >= gap && ShouldSwap(result[j - gap], temp, sortBy); j -= gap)
                    {
                        result[j] = result[j - gap];
                    }

                    result[j] = temp;
                }
            }

            return ApplySortDirection(result, sortDirection);
        }
    }
} 