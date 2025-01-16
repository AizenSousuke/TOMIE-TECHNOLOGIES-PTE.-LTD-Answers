using Microsoft.AspNetCore.Mvc;

namespace TomieTechnologies.Services
{
    public interface IOrderService
    {
        public Task<IActionResult> GetProducts(CancellationToken cancellationToken = default);
        public Task<IActionResult> FilterInStockProducts(CancellationToken cancellationToken = default);
    }
}