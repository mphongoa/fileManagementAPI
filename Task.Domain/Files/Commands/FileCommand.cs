using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Domain.Files.Commands
{
    public class FileCommand
    {
        /*public FileCommand(string name, string size, DateTime? dateOfUpload, string fileContant)
        {
           
            Name = name;
            Size = size;
            DateOfUpload = dateOfUpload;
            FileContant = fileContant;
           
        }
        */
        public int FileId { get; set; }
        public DateTime? DateOfUpload { get; }
        public string Name { get; }
        public string Size { get; }
        public string FileContant { get; }

        public IFormFile Files { get; set; }

    }
}
