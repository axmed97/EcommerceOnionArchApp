using Application.Abstraction.Storage;
using Application.Repositories.ProductImageFileRepositories;
using Application.Repositories.ProductRepositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageFileCommandHandler : IRequestHandler<UploadProductImageFileCommandRequest, UploadProductImageFileCommandResponse>
    {
        private readonly IStorageService _storageService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        public UploadProductImageFileCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageFileCommandResponse> Handle(UploadProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _storageService.UploadAsync("resource/files", request.FormFiles);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(x => new Domain.Entities.ProductImageFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
