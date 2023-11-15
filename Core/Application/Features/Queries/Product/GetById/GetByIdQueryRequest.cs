using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Product.GetById
{
    public class GetByIdQueryRequest : IRequest<GetByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
