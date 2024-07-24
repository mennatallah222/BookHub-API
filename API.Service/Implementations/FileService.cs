using API.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API.Service.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(string location, IFormFile file)
        {
            //get the path then the extension of the file, then create a new file name for it
            var path = _webHostEnvironment.WebRootPath + "/" + location + "/";
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;

            if (file.Length > 0)
            {
                try
                {

                    //if there's a file exists then if the dir exists, then create a path for it using its name too
                    //copy it to the location and clear the buffer of the stream using flush

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"/{location}/{fileName}";
                    }
                }

                catch (Exception ex)
                {
                    return "FailedToUploadTheImage";

                }
            }
            else
            {
                return "NoImage";
            }

        }
    }
}
