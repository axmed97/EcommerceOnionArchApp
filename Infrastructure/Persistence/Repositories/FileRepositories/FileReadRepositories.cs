using Application.Repositories.FileRepositories;
using Persistence.Context;

namespace Persistence.Repositories.FileRepositories
{
    public class FileReadRepositories : ReadRepository<Domain.Entities.File>, IFileReadRepositories
    {
        public FileReadRepositories(AppDbContext context) : base(context)
        {
        }
    }
}
