using Application.Abstraction.Storage;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
             => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetAllFiles(string pathOrContainerName)
            => _storage.GetAllFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles)
            => _storage.UploadAsync(pathOrContainerName, formFiles);
    }
}
