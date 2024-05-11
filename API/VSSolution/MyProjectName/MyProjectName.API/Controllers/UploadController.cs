using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProjectName.Manager.Interface;
using MyProjectName.Model;
using MyProjectName.Utility;
using System.Collections.Generic;


namespace MyProjectName.API.Controllers
{
	[Authorize]
    [ApiController]
    public class UploadController : ControllerBase
    {
        IUploadManager Manager;
        ValidationResult ValidationResult;
        ILog log4Net;
        public UploadController(IUploadManager manager)
        {
            Manager = manager;
            log4Net = this.Log<UploadController>();
            ValidationResult = new ValidationResult();
        }
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute + "/upload")]
        public ActionResult Post(List<IFormFile> files)
        {
            log4Net.Debug("IFormFileCount=" + files.Count);
            log4Net.Debug("HttpContext.Request.Form.Files=" + HttpContext.Request.Form.Files.Count);
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                List<IFormFile> imageList = new List<IFormFile>();
                var filesFromHttp = HttpContext.Request.Form.Files;
                foreach (var file in filesFromHttp)
                {
                    imageList.Add(file);
                }
                return Ok(Manager.UploadImages(imageList));
            }
            else
            {
                return Ok(Manager.UploadImages(files));
            }
        }
    }
}

