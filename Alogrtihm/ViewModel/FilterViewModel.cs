namespace Alogrtihm.ViewModel
{
    public class FilterViewModel
    {
        public string CurrentCategory { get; set; }
        public string CurrentBrand { get; set; }
        public decimal? CurrentMinPrice { get; set; }
        public decimal? CurrentMaxPrice { get; set; }
        public double? CurrentMinRating { get; set; }
        public string CurrentSortBy { get; set; }
        public string CurrentSortDirection { get; set; }
        public string CurrentFeatures { get; set; }
        public bool? CurrentInStock { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Brands { get; set; }
        public List<string> AllFeatures { get; set; }
    }

}
