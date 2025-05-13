using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class HeapSort : BaseSortAlgorithm
    {
        public override string Name => "Heap Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);

            // Build heap (rearrange array)
            for (int i = result.Count / 2 - 1; i >= 0; i--)
                Heapify(result, result.Count, i, sortBy);

            // One by one extract an element from heap
            for (int i = result.Count - 1; i > 0; i--)
            {
                // Move current root to end
                Swap(result, 0, i);

                // call max heapify on the reduced heap
                Heapify(result, i, 0, sortBy);
            }

            return ApplySortDirection(result, sortDirection);
        }

        private void Heapify(List<Product> products, int n, int i, string sortBy)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && ShouldSwap(products[largest], products[left], sortBy))
                largest = left;

            if (right < n && ShouldSwap(products[largest], products[right], sortBy))
                largest = right;

            if (largest != i)
            {
                Swap(products, i, largest);
                Heapify(products, n, largest, sortBy);
            }
        }
    }
} 