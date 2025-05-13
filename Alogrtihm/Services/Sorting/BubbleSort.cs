using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class BubbleSort : BaseSortAlgorithm
    {
        public override string Name => "Bubble Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);
            bool swapped;

            for (int i = 0; i < result.Count - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < result.Count - i - 1; j++)
                {
                    if (ShouldSwap(result[j], result[j + 1], sortBy))
                    {
                        Swap(result, j, j + 1);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }

            return ApplySortDirection(result, sortDirection);
        }
    }
} 