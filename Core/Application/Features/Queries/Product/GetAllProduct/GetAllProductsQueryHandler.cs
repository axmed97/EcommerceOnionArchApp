using Application.Repositories.ProductRepositories;
using MediatR;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(tracking: false).Count();
            var products = _productReadRepository.GetAll(tracking: false).Select(x => new
            {
                x.Id,
                x.ProductName,
                x.Quantity,
                x.Price,
                x.CreatedDate,
                x.UpdatedDate
            }).Skip(request.Page * request.Size).Take(request.Size).ToList();

            return new GetAllProductsQueryResponse()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
