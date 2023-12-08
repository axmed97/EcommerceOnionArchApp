using Application.Abstraction.Services;
using Application.DTOs.Order;
using Application.Repositories.OrderRepositories;

namespace Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDTO.Address,
                Id = Guid.Parse(createOrderDTO.BasketId),
                Description = createOrderDTO.Description,
            });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
