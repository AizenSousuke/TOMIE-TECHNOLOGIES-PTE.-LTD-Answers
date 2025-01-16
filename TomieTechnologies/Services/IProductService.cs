using Microsoft.AspNetCore.Mvc;

namespace TomieTechnologies.Services
{
    public interface IProductService
    {
        Task<IActionResult> GetProducts(CancellationToken cancellationToken = default);
        Task<IActionResult> FilterInStockProducts(CancellationToken cancellationToken = default);
    }
}