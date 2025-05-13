using Algorithm.Models;
using Alogrtihm.Services;
using Algorithm.Services.Sorting;

namespace Algorithm.Services
{
    public class SortingService : ISortingService
    {
        private readonly Dictionary<string, ISortAlgorithm> _sortAlgorithms;
        private string ss;
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

        public List<Product> SortProducts(List<Product> products, string sortBy, string sortDirection, string sortAlgorithm)
        {
            // Use the algorithm passed as a parameter or default to quick sort
            var algorithm = _sortAlgorithms.ContainsKey(sortAlgorithm.ToLower())
                            ? _sortAlgorithms[sortAlgorithm.ToLower()]
                            : _sortAlgorithms["quick"];
            ss = sortAlgorithm;
            return algorithm.Sort(products, sortBy, sortDirection);
        }


        public List<Product> SortByPrice(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "price", sortDirection , ss);
        }

        public List<Product> SortByRating(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "rating", sortDirection , ss);
        }

        public List<Product> SortByName(List<Product> products, string sortDirection)
        {
            return SortProducts(products, "name", sortDirection, ss);
        }
    }
}