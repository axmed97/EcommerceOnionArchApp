using Application.Abstraction.Storage;
using Application.Repositories.ProductImageFileRepositories;
using Application.Repositories.ProductRepositories;
using Application.RequestPagination;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IStorageService _storageService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(VM_Create_Product product)
        {
            await _productWriteRepository.AddAsync(new Product
            {
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                Price = product.Price,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(tracking: false).Count();
            var products = _productReadRepository.GetAll(tracking:false).Select(x => new
            {
                x.Id,
                x.ProductName,
                x.Quantity,
                x.Price,
                x.CreatedDate,
                x.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, tracking: false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(VM_Update_Product product)
        {
            Product getProduct = await _productReadRepository.GetByIdAsync(product.Id, tracking: true);
            getProduct.Quantity = product.Quantity;
            getProduct.Price = product.Price;
            product.ProductName = product.ProductName;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok(new
            {
                message = "Deleted successfully"
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFileCollection formFiles)
        {
            var datas = await _storageService.UploadAsync("resource/files", formFiles);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
