using Microsoft.AspNetCore.Mvc;
using TomieTechnologies.Services;

namespace TomieTechnologies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController
    {
        private readonly IOrderService _orderService;

        public ProductController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchProducts(CancellationToken cancellationToken = default)
        {
            var result = await _orderService.GetProducts(cancellationToken);

            return result;
        }

        [HttpGet]
        [Route("availability")]
        public async Task<IActionResult> FilterInStockProducts(CancellationToken cancellationToken = default)
        {
            var result = await _orderService.FilterInStockProducts(cancellationToken);

            return result;
        }
    }
}