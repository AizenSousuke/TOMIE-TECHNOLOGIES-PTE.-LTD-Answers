using Microsoft.AspNetCore.Mvc;

namespace TomieTechnologies.Services
{
    public class OrderService : IOrderService
    {
        public async Task<IActionResult> CreateOrder(int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}