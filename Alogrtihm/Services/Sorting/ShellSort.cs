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

            // Start with a big gap, then reduce the gap
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Do a gapped insertion sort for this gap size.
                // The first gap elements a[0..gap-1] are already in gapped order
                // keep adding one more element until the entire array is gap sorted
                for (int i = gap; i < n; i++)
                {
                    // add a[i] to the elements that have been gap sorted
                    // save a[i] in temp and make a hole at position i
                    var temp = result[i];

                    // shift earlier gap-sorted elements up until the correct
                    // location for a[i] is found
                    int j;
                    for (j = i; j >= gap && ShouldSwap(result[j - gap], temp, sortBy); j -= gap)
                    {
                        result[j] = result[j - gap];
                    }

                    // put temp (the original a[i]) in its correct location
                    result[j] = temp;
                }
            }

            return ApplySortDirection(result, sortDirection);
        }
    }
} 