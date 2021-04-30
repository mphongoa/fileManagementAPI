using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Files.Commands;
using Task.Infrastructure.Services.Files;

namespace Task.Application.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {

        private readonly IFileService _fileService;
        private static IWebHostEnvironment _webHostEnvironment;

           public FileController(IFileService fileService, IWebHostEnvironment webHostEnvironment)
          {
              _fileService = fileService;
              _webHostEnvironment = webHostEnvironment;
          }
        
        [HttpPost("upload")]
        public string UploadFile([FromForm] FileCommand addFile)
        {
            try
            {
                // && addFile.files.ContentType == ContentType.
               
                var fileExtention = Path.GetExtension(addFile.Files.FileName);

                ///FileStream fileStream111 = new FileStream(addFile.files.FileName, FileMode.Open);
               
              //  FileInfo info = new FileInfo(addFile.Files.FileName);


               // using var fs = new FileStream(addFile.Files.FileName, FileMode.Open, FileAccess.Read);
               // using var sr = new StreamReader(fs, Encoding.UTF8);

              //  string content =  sr.ReadLine();


                if (addFile.Files.Length > 0 && fileExtention == ".txt" )
                {
                    // string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", addFile.Files.FileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using(FileStream fileStream = System.IO.File.Create(path + addFile.Files.FileName))
                    {
                        addFile.Files.CopyTo(fileStream);
                        fileStream.Flush();

                        var b = addFile.Files.OpenReadStream();
                      // var c = b.Read.rea;
                      // to impliment  saving the file info in database
                        return "uploaded" + addFile.Files.Length; 
                    }
                }
                else
                {
                    return "Not Uploaded";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpDelete("Delete")]
        public void DeleteFile([FromForm] int id)
        {
            
            
        }


        [HttpGet("Files")]
        public void Files()
        {
            
        }

    }
}
