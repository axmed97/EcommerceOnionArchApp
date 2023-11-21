using Application.Repositories.ProductRepositories;
using Domain.Entities;
using Google.Apis.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var getProduct = await _productReadRepository.GetByIdAsync(request.Id, tracking: true);
            getProduct.Quantity = request.Quantity;
            getProduct.Price = request.Price;
            getProduct.ProductName = request.ProductName;
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Product Updated...");
            return new();
        }
    }
}
