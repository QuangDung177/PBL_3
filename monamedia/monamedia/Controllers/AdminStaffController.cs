using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Controllers
{
    public class AdminStaffController : Controller
    {
        public ActionResult LoginForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            using(AppDbContext db=new AppDbContext())
            {
                Account account=db.Accounts.FirstOrDefault(a=>(a.userName == username&&a.password==password&&(a.role=="Staff"||a.role=="Admin")));
                if (account != null)
                {
                    Session["AccountID"] = account.accountID;
                    return Json(new { success = true, message = "Đăng nhập thành công", role = account.role });
                }
                else
                    return Json(new { success = false, message = "Đăng nhập thất bại" });
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(new { success = true, message = "Da xoa session"});
        }
    }
}