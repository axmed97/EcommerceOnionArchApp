using Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _basketService.GetBasketItemsAsync();
            return response.Select(x => new GetBasketItemsQueryResponse()
            {
                BasktemItemId = x.Id.ToString(),
                ProductName = x.Product.ProductName,
                Price = x.Product.Price,
                Quantity = x.Product.Quantity
            }).ToList();
        }
    }
}
