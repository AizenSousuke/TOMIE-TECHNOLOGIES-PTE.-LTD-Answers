using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TomieTechnologies.Models;

namespace TomieTechnologies.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private List<ProductDetailsModel> _cachedProducts = new List<ProductDetailsModel>();

        public OrderService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _httpClient.BaseAddress = new Uri("https://dummyjson.com");
        }

        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("/products", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiModel = JsonSerializer.Deserialize<ApiProductResponseModel>(json, options);

                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ApiProductModel, ProductDetailsModel>()
                        .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                        .ForMember(dest => dest.Price,
                            opt => opt.MapFrom(src => src.Price))
                        .ForMember(dest => dest.Availability, opt => opt.MapFrom(src => src.Stock));
                });

                var listOfProducts = mapper.CreateMapper().Map<List<ProductDetailsModel>>(apiModel?.Products);
                _cachedProducts = listOfProducts;

                return new OkObjectResult(listOfProducts);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> FilterInStockProducts(CancellationToken cancellationToken = default)
        {
            if (_cachedProducts.Count == 0)
            {
                await GetProducts(cancellationToken);
            }

            if (_cachedProducts.Count > 0)
            {
                foreach (var product in _cachedProducts)
                {
                    if (product.Id % 2 == 0)
                    {
                        product.Availability = 0;
                    }
                    else
                    {
                        product.Availability = 1;
                    }
                }

                return new OkObjectResult(_cachedProducts.Where(src => src.Availability >= 1).ToList());
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}