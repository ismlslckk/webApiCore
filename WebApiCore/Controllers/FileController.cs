using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using WebApiCore.Models;
using WebApiCore.Models.File;
using WebApiCore.UtilityClasses;

namespace WebApiCore.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(RequestFile upload)
        {
            var uploaded = new FilesViewModel();
            try
            {
                if (upload == null || upload.Files.Count == 0)
                    return StatusCode((int) HttpStatusCode.BadRequest, "files not selected");

                foreach (var file in upload.Files)
                {
                    Guid g = Guid.NewGuid();
                     
                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Files",
                        g+ Path.GetExtension(file.FileName));

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploaded.Files.Add(
                        new FileDetails { Name = file.FileName, Path = Request.Host.ToString()+"/Files/"+g + Path.GetExtension(file.FileName) });
                }

                return Ok(uploaded);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,exception.ToString());

            }

        }

        [HttpGet("getFile")]
        public async Task<IActionResult> GetFile(string category)
        {

            string contentRootPath = Request.Host+"/"+ _hostingEnvironment.ContentRootPath;

            if (category == null)
                return Content("filename not present");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files", category);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream"); 
        }
    }
}