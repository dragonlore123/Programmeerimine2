using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _filesRoot;

        public LocalFileClient(string filesRoot)
        {
            _filesRoot = Path.GetFullPath(filesRoot);
        }

        public async Task Save(string fileName, string fileStore, Stream inputStream)
        {
            var folderPath = GetFolderPath(fileStore);
            var filePath = GetFilePath(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await inputStream.CopyToAsync(fileStream);
        }

        public Task<bool> Delete(string fileName, string fileStore)
        {
            var filePath = GetFilePath(GetFolderPath(fileStore), fileName);

            if (!File.Exists(filePath))
            {
                return Task.FromResult(false);
            }

            File.Delete(filePath);
            return Task.FromResult(true);
        }

        public Task<Stream> Get(string fileName, string fileStore)
        {
            var filePath = GetFilePath(GetFolderPath(fileStore), fileName);

            if (!File.Exists(filePath))
            {
                return Task.FromResult<Stream>(null);
            }

            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return Task.FromResult(stream);
        }

        public Task<IList<string>> List(string fileStore)
        {
            var folderPath = GetFolderPath(fileStore);

            if (!Directory.Exists(folderPath))
            {
                return Task.FromResult<IList<string>>(new List<string>());
            }

            IList<string> files = Directory.GetFiles(folderPath)
                .Select(path => Path.GetFileName(path))
                .OrderBy(name => name)
                .ToList();

            return Task.FromResult(files);
        }

        private string GetFolderPath(string fileStore)
        {
            var folderPath = Path.GetFullPath(Path.Combine(_filesRoot, fileStore));
            EnsureInsideRoot(folderPath);
            return folderPath;
        }

        private string GetFilePath(string folderPath, string fileName)
        {
            // Failinimi ei tohi sisaldada kaustu (nt ../)
            if (string.IsNullOrWhiteSpace(fileName) || fileName != Path.GetFileName(fileName))
            {
                throw new ArgumentException("Invalid file name.", nameof(fileName));
            }

            var filePath = Path.GetFullPath(Path.Combine(folderPath, fileName));
            EnsureInsideRoot(filePath);
            return filePath;
        }

        private void EnsureInsideRoot(string path)
        {
            if (!path.StartsWith(_filesRoot, StringComparison.Ordinal))
            {
                throw new ArgumentException("Path is outside of files root.");
            }
        }
    }
}
