using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using MyProjectName.Manager.Interface;
using MyProjectName.Model;
using MyProjectName.Utility;
using log4net;


namespace MyProjectName.API.Controllers
{
	[Authorize]
    [ApiController]
    public class SPController : ControllerBase
    {   ILog log4Net;
        ISPManager Manager;
        ValidationResult ValidationResult;
        public SPController(ISPManager manager)
        {  
			log4Net = this.Log<SPController>();
            Manager = manager;
            ValidationResult = new ValidationResult();
        }

        [HttpPost]
[Route(APIEndpoint.DefaultRoute + "/Sp_Login")]
public ActionResult Post_Sp_Login(Sp_LoginInputModel model)
{
    try
    {
        return Ok(Manager.Sp_Login(model));
    }
     catch (Exception ex)
   {
        return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
    }
}
[HttpPost]
[Route(APIEndpoint.DefaultRoute + "/Sp_Reg")]
public ActionResult Post_Sp_Reg(Sp_RegInputModel model)
{
    try
    {
        return Ok(Manager.Sp_Reg(model));
    }
     catch (Exception ex)
   {
        return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
    }
}
        
    }
}

