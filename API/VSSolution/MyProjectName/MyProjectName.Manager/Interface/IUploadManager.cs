using Microsoft.AspNetCore.Http;
using MyProjectName.Model;
using MyProjectName.Utility;
using System.Collections.Generic;

namespace MyProjectName.Manager.Interface
{
    public interface IUploadManager
    {
        APIResponse UploadImages(List<IFormFile> images);
    }
}

