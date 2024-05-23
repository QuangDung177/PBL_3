using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        [HttpPost]
        public ActionResult SendEmail(string email)
        {
            //zrze bskk zmlm znbx
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            Random random = new Random();
            string newpassword= new string(Enumerable.Repeat(validChars,12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
            AppDbContext db = new AppDbContext();
            foreach(var i in db.Accounts)
            {
                if(i.email == email)
                {
                    i.password = newpassword;
                }
            }
            db.SaveChanges();
            var fromAddress = new MailAddress("dattran10102k4@gmail.com");
            var toAddress = new MailAddress(email);
            const string frompass = "zrze bskk zmlm znbx";
            const string subject = "Mật khẩu mới";
            string body = newpassword;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, frompass),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
                return Json(new { success = true, message = "Gui mat khau moi thanh cong", newpassword });
            }
        }
    }
}