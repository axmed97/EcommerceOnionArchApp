using Application.Repositories.ProductImageFileRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Product.ChangeShowCaseImage
{
    public class ChangeShowCaseImageCommandHandler : IRequestHandler<ChangeShowCaseImageCommandRequest, ChangeShowCaseImageCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowCaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowCaseImageCommandResponse> Handle(ChangeShowCaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                .Include(x => x.Products)
                .SelectMany(x => x.Products, (p, x) => new
                {
                    p,
                    x
                });
                
            var data = await query.FirstOrDefaultAsync(x => x.x.Id == Guid.Parse(request.ProductId) && x.p.ShowCase);
            if (data != null) data.p.ShowCase = false;

            var image = await query.FirstOrDefaultAsync(x => x.x.Id == Guid.Parse(request.ImageId));
            if(image != null)
                image.p.ShowCase = true;
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
