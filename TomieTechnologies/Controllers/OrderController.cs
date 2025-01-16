using Microsoft.AspNetCore.Mvc;
using TomieTechnologies.Services;

namespace TomieTechnologies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(int productId, int quantity, CancellationToken cancellationToken = default)
        {
            var result = await _orderService.CreateOrder(productId, quantity);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken = default)
        {
            return await _orderService.GetOrders(cancellationToken);
        }
    }
}