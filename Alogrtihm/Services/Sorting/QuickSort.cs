using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class QuickSort : BaseSortAlgorithm
    {
        public override string Name => "Quick Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);
            QuickSortInternal(result, 0, result.Count - 1, sortBy);

            return ApplySortDirection(result, sortDirection);
        }

        private void QuickSortInternal(List<Product> products, int low, int high, string sortBy)
        {
            if (low < high)
            {
                int partitionIndex = Partition(products, low, high, sortBy);
                QuickSortInternal(products, low, partitionIndex - 1, sortBy);
                QuickSortInternal(products, partitionIndex + 1, high, sortBy);
            }
        }

        private int Partition(List<Product> products, int low, int high, string sortBy)
        {
            var pivot = products[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (ShouldSwap(products[j], pivot, sortBy))
                {
                    i++;
                    Swap(products, i, j);
                }
            }

            Swap(products, i + 1, high);
            return i + 1;
        }
    }
} 