using Application.Repositories.ProductImageFileRepositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories.ProductImageFileRepositories
{
    public class ProductImageReadFileRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageReadFileRepository(AppDbContext context) : base(context)
        {
        }
    }
}
