using Algorithm.Models;
using Algorithm.Services;
using Alogrtihm.ViewModel;
using Alogrtihm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Algorithm.Controllers
{
    public class ProductController : Controller
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ISortingService _sortingService;

        #endregion

        #region Constructor

        public ProductController(
            IProductService productService,
            ISortingService sortingService)
        {
            _productService = productService;
            _sortingService = sortingService;
        }

        #endregion

        #region Actions

        public ActionResult Index(
            string category,
            string brand,
            decimal? minPrice,
            decimal? maxPrice,
            double? minRating,
            string sortBy = "name",
            string sortDirection = "asc",
            string sortAlgorithm = "quick",
            string features = "",
            bool? inStock = null,
            int page = 1,
            int pageSize = 4)
        {
            // Parse features list from comma-separated string
            var featuresList = string.IsNullOrWhiteSpace(features)
                ? new List<string>()
                : features.Split(',').Select(f => f.Trim()).ToList();

            // Generate cache key (optional, could be used later)
            string cacheKey = $"{category}_{brand}_{minPrice}_{maxPrice}_{minRating}_{sortBy}_{sortDirection}_{sortAlgorithm}_{features}_{inStock}_{page}_{pageSize}";

            // Fetch all products
            var allProducts = _productService.GetAllProducts();

            // Build filtering metadata
            var filterViewModel = new FilterViewModel
            {
                CurrentCategory = category,
                CurrentBrand = brand,
                CurrentMinPrice = minPrice,
                CurrentMaxPrice = maxPrice,
                CurrentMinRating = minRating,
                CurrentSortBy = sortBy,
                CurrentSortDirection = sortDirection,
                CurrentSortAlgorithm = sortAlgorithm,
                CurrentFeatures = features,
                CurrentInStock = inStock,
                CurrentPage = page,
                PageSize = pageSize,
                Categories = allProducts.Select(p => p.Category).Distinct().ToList(),
                Brands = allProducts.Select(p => p.Brand).Distinct().ToList(),
                AllFeatures = allProducts.SelectMany(p => p.Features).Distinct().ToList()
            };

            // Apply filtering
            var filteredProducts = _productService.FilterProducts(
                allProducts, category, brand, minPrice, maxPrice, minRating, featuresList, inStock);

            int totalItems = filteredProducts.Count;
            filterViewModel.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Sort the filtered products
            var sortedProducts = _sortingService.SortProducts(filteredProducts, sortBy, sortDirection);

            // Paginate the sorted products
            var paginatedProducts = sortedProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Build final view model
            var productViewModel = new ProductViewModel
            {
                Products = paginatedProducts,
                TotalProducts = totalItems,
                PriceRange = new PriceRange
                {
                    Min = allProducts.Min(p => p.Price),
                    Max = allProducts.Max(p => p.Price),
                    CurrentMin = minPrice ?? allProducts.Min(p => p.Price),
                    CurrentMax = maxPrice ?? allProducts.Max(p => p.Price)
                }
            };

            ViewBag.FilterViewModel = filterViewModel;
            return View(productViewModel);
        }

        public IActionResult Algorithms()
        {
            return View();
        }

        #endregion
    }
}
