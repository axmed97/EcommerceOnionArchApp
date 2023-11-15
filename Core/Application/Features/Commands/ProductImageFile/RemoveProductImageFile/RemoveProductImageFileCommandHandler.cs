using Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileCommandHandler : IRequestHandler<RemoveProductImageFileCommandRequest, RemoveProductImageFileCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageFileCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<RemoveProductImageFileCommandResponse> Handle(RemoveProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table
            .Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.ProductId));

            var productImageFile = product.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(request.ImageId));

            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
