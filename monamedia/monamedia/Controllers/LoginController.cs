using monamedia.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(string username, string password, string email,  string fullName, DateTime birthDate, int gender, string address, string phoneNumber)
        {
            try
            {
                string result = await _loginService.RegisterAsync(username, password, email, fullName, birthDate, gender, address, phoneNumber);
                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();
                if (code == 1 && message == "SP Executed Successfully")
                {
                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "Đăng ký không thành công. " + message;
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thực hiện đăng ký. Vui lòng thử lại sau.";
                return View("Index");
            }
        }


    }
}