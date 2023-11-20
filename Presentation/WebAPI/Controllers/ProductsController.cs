using Application.Features.Commands.Product.CreateProduct;
using Application.Features.Commands.Product.UpdateProduct;
using Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using Application.Features.Queries.Product.GetAllProduct;
using Application.Features.Queries.Product.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
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

        [HttpGet("[action]/{Id}")]
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
        public async Task<IActionResult> GetProductImages([FromRoute] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            var response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{ProductId}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageFileCommandRequest removeProductImageFileCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageFileCommandRequest.ImageId = imageId;
            var response = await _mediator.Send(removeProductImageFileCommandRequest);
            return Ok();
        }
    }
}
