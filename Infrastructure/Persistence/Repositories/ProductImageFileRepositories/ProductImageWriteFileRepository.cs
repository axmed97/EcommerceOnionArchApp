using Application.Repositories.ProductImageFileRepositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories.ProductImageFileRepositories
{
    public class ProductImageWriteFileRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageWriteFileRepository(AppDbContext context) : base(context)
        {
        }
    }
}
