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
    public class SignController : Controller
    {
        // GET: Sign
        public ActionResult Index()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        public async Task<ActionResult> Index(string username, string password)
        {
            try
            {
                // Gọi service hoặc API để kiểm tra đăng nhập
                var loginService = new LoginService();
                string result = await loginService.LoginAsync(username, password);

                // Phân tích phản hồi JSON từ service hoặc API
                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                // Kiểm tra kết quả đăng nhập
                if (code == 1 && message == "SP Executed Successfully")
                {
                    // Lấy giá trị từ trường "Result"
                    string loginResult = jsonResponse["document"]["ResultTable1"][0]["Result"].Value<string>();

                    // Kiểm tra giá trị đăng nhập
                    if (loginResult == "Đăng nhập thành công")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Đăng nhập không thành công. " + loginResult;
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Đăng nhập không thành công. " + message;
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thực hiện đăng nhập. Vui lòng thử lại sau.";
                return View();
            }
        }

    }
}