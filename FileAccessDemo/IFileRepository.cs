using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace FileAccessDemo
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileItem>> GetFilesAsync();
        Task<string> GetFileContentAsync(FileItem storageFile);
    }


    public class FileRepository : IFileRepository
    {
        private readonly ILoggerService loggerService;
        private readonly StorageFolder storageFolder;

        public FileRepository(ILoggerService loggerService)
        {
            this.storageFolder = KnownFolders.DocumentsLibrary;
            this.loggerService = loggerService;
        }
        
        
        public async Task<IEnumerable<FileItem>> GetFilesAsync()
        {
            loggerService.Log("retrieving files");        
            var files = await storageFolder.GetFilesAsync();

            return files.Select(x => new FileItem(x.Name, x.DisplayName));
        }

        public async Task<string> GetFileContentAsync(FileItem fileItem)
        {
            var storageFile = await storageFolder.GetFileAsync(fileItem.Code);
            return await FileIO.ReadTextAsync(storageFile);
        }
    }   
}
