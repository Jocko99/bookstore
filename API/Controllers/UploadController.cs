using API.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // POST api/<UploadController>
        [HttpPost]
        public IActionResult Post([FromForm] UploadDto dto)
        {
            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(dto.Image.FileName);

            var newFileName = guid + extension;

            var path = Path.Combine("../../my-app/public/assets", "images", newFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                dto.Image.CopyTo(fileStream);
            }

            return Ok(newFileName);
        }
        [HttpDelete("/api/upload/photo/delete/{filename}")]
        public IActionResult DeleteFile(string filename)
        {
            if(!string.IsNullOrEmpty(filename) && !string.IsNullOrWhiteSpace(filename))
            {
                string findImage = Path.Combine("../../my-app/public/assets", "Images", filename);
                if ((System.IO.File.Exists(findImage)))
                {
                    System.IO.File.Delete(findImage);
                }
                else
                {
                    return StatusCode(404, new { message = "File doesn't exists." });
                }
                return NoContent();
            }
            else
            {
                return StatusCode(422, new { message="File name is required." });
            }
        }
    }
}
