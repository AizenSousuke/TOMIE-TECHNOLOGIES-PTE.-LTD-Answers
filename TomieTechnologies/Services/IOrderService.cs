using Microsoft.AspNetCore.Mvc;

namespace TomieTechnologies.Services
{
    public interface IOrderService
    {
        Task<IActionResult> CreateOrder(int productId, int quantity);
    }
}