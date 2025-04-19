using Algorithm.Models;
using Algorithm.Services;
using Microsoft.AspNetCore.Mvc;
using Alogrtihm.ViewModel;
using Alogrtihm.Services;

namespace Algorithm.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISortingService _sortingService;

        public ProductController(IProductService productService, ISortingService sortingService)
        {
            _productService = productService;
            _sortingService = sortingService;
        }

        public ActionResult Index(string category, string brand, decimal? minPrice, decimal? maxPrice,
                            double? minRating, string sortBy = "name", string sortDirection = "asc",
                            string features = "", bool? inStock = null, int page = 1, int pageSize = 4)
        {
            var featuresList = !string.IsNullOrEmpty(features)
                ? features.Split(',').Select(f => f.Trim()).ToList()
                : new List<string>();

            string cacheKey = $"{category}_{brand}_{minPrice}_{maxPrice}_{minRating}_{sortBy}_{sortDirection}_{features}_{inStock}_{page}_{pageSize}";

            var products = _productService.GetAllProducts();

            var filterViewModel = new FilterViewModel
            {
                CurrentCategory = category,
                CurrentBrand = brand,
                CurrentMinPrice = minPrice,
                CurrentMaxPrice = maxPrice,
                CurrentMinRating = minRating,
                CurrentSortBy = sortBy,
                CurrentSortDirection = sortDirection,
                CurrentFeatures = features,
                CurrentInStock = inStock,
                CurrentPage = page,
                PageSize = pageSize,
                Categories = products.Select(p => p.Category).Distinct().ToList(),
                Brands = products.Select(p => p.Brand).Distinct().ToList(),
                AllFeatures = products.SelectMany(p => p.Features).Distinct().ToList()
            };

            var filtered = _productService.FilterProducts(products, category, brand, minPrice, maxPrice, minRating, featuresList, inStock);
            int totalItems = filtered.Count;

            // Calculate total pages
            filterViewModel.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Sort products
            filtered = _sortingService.SortProducts(filtered, sortBy, sortDirection);

            // Apply pagination
            var paginatedResult = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new ProductViewModel
            {
                Products = paginatedResult,
                TotalProducts = totalItems,
                PriceRange = new PriceRange
                {
                    Min = products.Min(p => p.Price),
                    Max = products.Max(p => p.Price),
                    CurrentMin = minPrice ?? products.Min(p => p.Price),
                    CurrentMax = maxPrice ?? products.Max(p => p.Price)
                }
            };
            ViewBag.FilterViewModel = filterViewModel;
            return View(viewModel);
        }

   
    }
}