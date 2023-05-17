using HeyRed.Mime;
using Secrets_Sharing_BE.Interfaces.Services;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Services
{
    public class FileDataService : IFileDataService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileDataService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public FileResponse? Download(string filename)
        {
            try
            {
                var directory = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles", filename);
                string contenttype = MimeTypesMap.GetMimeType(filename);
                var bytes = File.ReadAllBytes(directory);
                var data = new FileResponse()
                {
                    Data = bytes,
                    ContentType = contenttype
                };
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void DeleteFile(string filename, out string message)
        {
            try
            {
                var directory = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles", filename);
                if (File.Exists(directory))
                {
                    File.Delete(directory);
                }
                message = "File doesn't exist";
            }
            catch
            {
                message = "Error when deleting file";
            }
        }
        public FileData PrepCreate(IFormFile file, bool delete, out string error,int userid)
        {
            error = string.Empty;
            FileData dt = new()
            {
                FileName = file.FileName,
                AutoDelete = delete,
                UploadBy = userid,
                Protect = Guid.NewGuid().ToString("N"),//this string will protect the url
            };
            return dt;
        }
        public void Upload(IFormFile file, out string message)
        {
            message = string.Empty;
            try
            {
                string directory = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
                string filepath = Path.Combine(directory, file.FileName);
                using var stream = new FileStream(filepath, FileMode.Create);
                file.CopyTo(stream);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
        }
    }
}
