using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApiCore.Models.File
{
    public class RequestFile
    {
        public List<IFormFile> Files { get; set; }
        public string Category { get; set; }
    }
}
