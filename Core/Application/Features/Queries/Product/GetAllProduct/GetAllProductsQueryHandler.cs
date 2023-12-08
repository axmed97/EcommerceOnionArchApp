using Application.Repositories.ProductRepositories;
using Google.Apis.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductsQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Product Listed");
            var totalCount = _productReadRepository.GetAll(tracking: false).Count();

            var products = _productReadRepository.GetAll(tracking: false)
                .Skip(request.Page * request.Size).Take(request.Size)
                .Include(x => x.ProductImageFiles)
                .Select(x => new
            {
                x.Id,
                x.ProductName,
                x.Quantity,
                x.Price,
                x.CreatedDate,
                x.UpdatedDate,
                x.ProductImageFiles
            }).ToList();

            return new GetAllProductsQueryResponse()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
