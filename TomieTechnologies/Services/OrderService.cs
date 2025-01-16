using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TomieTechnologies.Classes;
using TomieTechnologies.Models;

namespace TomieTechnologies.Services
{
    public class OrderService : IOrderService
    {
        public async Task<IActionResult> CreateOrder(int productId, int quantity,
            CancellationToken cancellationToken = default)
        {
            var allProducts = ProductsDB.CachedProducts;
            if (allProducts.All(src => src.Id != productId))
            {
                return new BadRequestObjectResult("Product not found");
            }

            var productToOrder = ProductsDB.CachedProducts.Single(src => src.Id == productId);
            if (productToOrder.Availability == 0 || productToOrder.Availability < quantity)
            {
                return new BadRequestObjectResult(
                    $"Product with id: {productId} is out of stock or quantity requested is more than the available quantity");
            }

            var order = new OrderModel()
            {
                Id = productId,
                Name = productToOrder.Name,
                Quantity = quantity,
                Price = productToOrder.Price * quantity
            };

            OrdersDB.Orders.Add(order);

            allProducts.Single(src => src.Id == productId).Availability -= quantity;

            return new OkObjectResult($"Order created: {JsonSerializer.Serialize(order)}");
        }

        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken = default)
        {
            var allOrders = OrdersDB.Orders;

            return new OkObjectResult(allOrders);
        }
    }
}