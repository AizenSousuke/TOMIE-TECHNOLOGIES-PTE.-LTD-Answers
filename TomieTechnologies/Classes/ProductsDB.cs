using TomieTechnologies.Models;

namespace TomieTechnologies.Classes
{
    /// <summary>
    /// Fake DB
    /// </summary>
    public static class ProductsDB
    {
        public static List<ProductDetailsModel> CachedProducts { get; set; } = new List<ProductDetailsModel>();
    }
}