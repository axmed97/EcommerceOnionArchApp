using Application.Repositories.ProductRepositories;
using MediatR;

namespace Application.Features.Queries.Product.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id, tracking: false);
            return new()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}
