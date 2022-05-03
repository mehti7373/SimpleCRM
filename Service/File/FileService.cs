using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.File
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _environment;

        public FileService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveTicketFilesAsync(IFormFile formFile)
        {
            var ticket_files = System.IO.Path.Combine(_environment.WebRootPath, "ticket_files");
            if (!System.IO.Directory.Exists(ticket_files))
            {
                System.IO.Directory.CreateDirectory(ticket_files);
            }

            var oldName = formFile.FileName;
            var name = Guid.NewGuid().ToString();
            var fullName = name + System.IO.Path.GetExtension(formFile.FileName);

            var path = System.IO.Path.Combine("ticket_files", fullName);
            var fullPath = System.IO.Path.Combine(_environment.WebRootPath, path);

            using (var fileStream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return path;
        }
    }
}
