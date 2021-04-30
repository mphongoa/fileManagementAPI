using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Domain.BaseTypes;

namespace Task.Domain.Files
{
    public class File 
    {
        public int FileId { get; set; }
        public DateTime? DateOfUpload { get; }
        public string Name { get; set; }
        public string Size { get; set; }

        public string Path { get; set; }
        public string Contant { get; set; }
    }
}
