using Application.Repositories.FileRepositories;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.FileRepositories
{
    public class FileWriteRepositories : WriteRepository<Domain.Entities.File>, IFileWriteRepositories
    {
        public FileWriteRepositories(AppDbContext context) : base(context)
        {
        }
    }
}
