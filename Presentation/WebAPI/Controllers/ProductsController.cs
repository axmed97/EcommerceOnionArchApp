using Application.Abstraction.Storage;
using Application.Features.Commands.Product.CreateProduct;
using Application.Features.Commands.Product.UpdateProduct;
using Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using Application.Features.Queries.Product.GetAllProduct;
using Application.Features.Queries.Product.GetById;
using Application.Repositories.ProductImageFileRepositories;
using Application.Repositories.ProductRepositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(CreateProductCommandRequest createProductCommandRequest)
        {
            var response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            var response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute]GetByIdQueryRequest getByIdQueryRequest)
        {
            GetByIdQueryResponse response = await _mediator.Send(getByIdQueryRequest);
            return Ok(response);    
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            var response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]RemoveProductImageFileCommandRequest removeProductImageFileCommandRequest)
        {
            var response = await _mediator.Send(removeProductImageFileCommandRequest);   
            return Ok(new
            {
                message = "Deleted successfully"
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageFileCommandRequest uploadProductImageFileCommandRequest, IFormFileCollection formFiles)
        {
            uploadProductImageFileCommandRequest.FormFiles = formFiles;
            var response = await _mediator.Send(uploadProductImageFileCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return Ok(product.ProductImageFiles.Select(x => new
            {
                x.Path,
                x.FileName
            }));
        }

        [HttpDelete("[action]/{ProductId}")]
        public async Task<IActionResult> DeleteProductImage([FromQuery, FromRoute]RemoveProductImageFileCommandRequest removeProductImageFileCommandRequest)
        {
            var response = await _mediator.Send(removeProductImageFileCommandRequest);
            return Ok();
        }
    }
}
