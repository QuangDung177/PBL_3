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
        Models.Model2 db = new Models.Model2();

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
                var loginService = new LoginService();
                string result = await loginService.LoginAsync(username, password);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var customerInfo = jsonResponse["document"]["ResultTable1"][0];

                    var customer = new CustomerInfo
                    {
                        customerID = customerInfo["customerID"].Value<int>(),
                        AccountID = customerInfo["accountID"].Value<int>(),
                        FullName = customerInfo["fullName"].Value<string>(),
                        BirthDate = customerInfo["birthDate"].Value<string>(),
                        Address = customerInfo["address"].Value<string>(),
                        PhoneNumber = customerInfo["phoneNumber"].Value<string>()
                    };

                    // Lưu thông tin khách hàng vào session
                    Session["CustomerInfo"] = customer;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Đăng nhập không thành công. " + message;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thực hiện đăng nhập. Vui lòng thử lại sau.";
                return View();
            }
        }


        public ActionResult Edit()
        {
            if (Session["CustomerInfo"] != null)
            {
                var customerInfo = (CustomerInfo)Session["CustomerInfo"];
                return View(customerInfo);
            }
            else
            {
                // Xử lý khi session không tồn tại
                return RedirectToAction("Index", "Login"); // Ví dụ chuyển hướng đến trang đăng nhập
            }
        }
        [HttpPost]
        public ActionResult Save(CustomerInfo model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customerInfoToUpdate = db.Customers.FirstOrDefault(c => c.accountID == model.AccountID);
                    if (customerInfoToUpdate != null)
                    {
                        customerInfoToUpdate.fullName = model.FullName;
                        customerInfoToUpdate.birthDate = Convert.ToDateTime(model.BirthDate);
                        customerInfoToUpdate.address = model.Address;
                        customerInfoToUpdate.phoneNumber = model.PhoneNumber;

                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Đã xảy ra lỗi khi lưu thông tin. Vui lòng thử lại sau.";
                    return View("Edit", model);
                }
            }
            else
            {
                return View("Edit", model);
            }
        }


    }
}