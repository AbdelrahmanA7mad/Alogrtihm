using Algorithm.Models;

namespace Algorithm.Services.Sorting
{
    public class MergeSort : BaseSortAlgorithm
    {
        public override string Name => "Merge Sort";

        public override List<Product> Sort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            var result = new List<Product>(products);
            var temp = new List<Product>(products.Count);
            for (int i = 0; i < products.Count; i++)
                temp.Add(null);

            MergeSortInternal(result, temp, 0, result.Count - 1, sortBy);

            return ApplySortDirection(result, sortDirection);
        }

        private void MergeSortInternal(List<Product> products, List<Product> temp, int left, int right, string sortBy)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSortInternal(products, temp, left, middle, sortBy);
                MergeSortInternal(products, temp, middle + 1, right, sortBy);

                MergeArrays(products, temp, left, middle, right, sortBy);
            }
        }

        private void MergeArrays(List<Product> products, List<Product> temp, int left, int middle, int right, string sortBy)
        {
            // Copy to temp array
            for (int i = left; i <= right; i++)
                temp[i] = products[i];

            int i1 = left;
            int i2 = middle + 1;
            int current = left;

            // Merge back to original array
            while (i1 <= middle && i2 <= right)
            {
                if (ShouldSwap(temp[i1], temp[i2], sortBy))
                {
                    products[current] = temp[i1];
                    i1++;
                }
                else
                {
                    products[current] = temp[i2];
                    i2++;
                }
                current++;
            }

            // Copy remaining elements
            while (i1 <= middle)
            {
                products[current] = temp[i1];
                current++;
                i1++;
            }
        }
    }
} 