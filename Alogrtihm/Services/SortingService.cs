using Algorithm.Models;
using Alogrtihm.Services;

namespace Algorithm.Services
{
    public class SortingService : ISortingService
    {
        public List<Product> SortProducts(List<Product> products, string sortBy, string sortDirection)
        {
            switch (sortBy.ToLower())
            {
                case "price":
                    return SortByPrice(products, sortDirection);
                case "rating":
                    return SortByRating(products, sortDirection);
                case "name":
                    return SortByName(products, sortDirection);
                default:
                    return products;
            }
        }

        public List<Product> SortByPrice(List<Product> products, string sortDirection)
        {
            return QuickSort(new List<Product>(products), "price", sortDirection);
        }

        public List<Product> SortByRating(List<Product> products, string sortDirection)
        {
            return QuickSort(new List<Product>(products), "rating", sortDirection);
        }

        public List<Product> SortByName(List<Product> products, string sortDirection)
        {
            return MergeSort(new List<Product>(products), "name", sortDirection);
        }

        // QuickSort implementation for sorting products
        private List<Product> QuickSort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1) return products;

            // Create a new list to avoid modifying the original
            var result = new List<Product>(products);
            QuickSortInternal(result, 0, result.Count - 1, sortBy);

            // Reverse for descending order if needed
            if (sortDirection.ToLower() == "desc")
                result.Reverse();

            return result;
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
                bool shouldSwap = false;

                if (sortBy == "price")
                    shouldSwap = products[j].Price <= pivot.Price;
                else if (sortBy == "rating")
                    shouldSwap = products[j].Rating <= pivot.Rating;
                else // name
                    shouldSwap = string.Compare(products[j].Name, pivot.Name, StringComparison.OrdinalIgnoreCase) <= 0;

                if (shouldSwap)
                {
                    i++;
                    Swap(products, i, j);
                }
            }

            Swap(products, i + 1, high);
            return i + 1;
        }

        private void Swap(List<Product> products, int i, int j)
        {
            var temp = products[i];
            products[i] = products[j];
            products[j] = temp;
        }

        // MergeSort implementation for sorting products
        private List<Product> MergeSort(List<Product> products, string sortBy, string sortDirection)
        {
            if (products.Count <= 1)
                return products;

            // Create a copy to avoid modifying the original
            var result = new List<Product>(products);
            var temp = new List<Product>(products.Count);
            for (int i = 0; i < products.Count; i++)
                temp.Add(null);

            MergeSortInternal(result, temp, 0, result.Count - 1, sortBy);

            // Reverse for descending order if needed
            if (sortDirection.ToLower() == "desc")
                result.Reverse();

            return result;
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
                bool useLeft = false;

                if (sortBy == "price")
                    useLeft = temp[i1].Price <= temp[i2].Price;
                else if (sortBy == "rating")
                    useLeft = temp[i1].Rating <= temp[i2].Rating;
                else // name
                    useLeft = string.Compare(temp[i1].Name, temp[i2].Name, StringComparison.OrdinalIgnoreCase) <= 0;

                if (useLeft)
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