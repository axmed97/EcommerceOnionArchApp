using Application.Repositories.ProductRepositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new Domain.Entities.Product
            {
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                Price = request.Price,
            });
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
