using Algorithm.Models;
using Alogrtihm.Services;
using Algorithm.Services.Sorting;

namespace Algorithm.Services
{
    public class SortingService : ISortingService
    {
        private readonly Dictionary<string, ISortAlgorithm> _sortAlgorithms;

        public SortingService()
        {
            _sortAlgorithms = new Dictionary<string, ISortAlgorithm>
            {
                { "bubble", new BubbleSort() },
                { "insertion", new InsertionSort() },
                { "selection", new SelectionSort() },
                { "heap", new HeapSort() },
                { "shell", new ShellSort() },
                { "quick", new QuickSort() },
                { "merge", new MergeSort() }
            };
        }

        public List<Product> SortProducts(List<Product> products, string sortBy, string sortDirection)
        {
            // Default to quick sort if no algorithm specified
            var algorithm = _sortAlgorithms["quick"];
            return algorithm.Sort(products, sortBy, sortDirection);
        }

        public List<Product> SortByPrice(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "price", sortDirection);
        }

        public List<Product> SortByRating(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "rating", sortDirection);
        }

        public List<Product> SortByName(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "name", sortDirection);
        }
    }
}