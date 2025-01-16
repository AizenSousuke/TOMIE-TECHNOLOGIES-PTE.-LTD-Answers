using Microsoft.AspNetCore.Mvc;

namespace TomieTechnologies.Services
{
    public interface IOrderService
    {
        Task<IActionResult> CreateOrder(int productId, int quantity, CancellationToken cancellationToken = default);
        Task<IActionResult> GetOrders(CancellationToken cancellationToken = default);
    }
}