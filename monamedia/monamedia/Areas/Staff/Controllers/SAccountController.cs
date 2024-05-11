using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using monamedia.ViewModels;
using System.Net;
using System.Security.Policy;
using System.Web.Helpers;
using System.Xml.Linq;

namespace monamedia.Areas.Staff.Controllers
{
    public class SAccountController : Controller
    {
        // GET: Staff/SAccount
        public ActionResult AccountInfo()
        {
            int accountId = Convert.ToInt32(Session["AccountID"]);
            ViewBag.AccountId = accountId;
            return View();
        }
        public ActionResult GetAccountInfo(int id)
        {
            AppDbContext db = new AppDbContext();
            var accountDetails = (from a in db.Accounts
                                  join s in db.Staffs on a.accountID equals s.accountID
                                  where a.accountID == id
                                  select new
                                  {
                                      accountID = a.accountID,
                                      userName = a.userName,
                                      password = a.password,
                                      email = a.email,
                                      fullName = s.fullName,
                                      birthDate = s.birthDate,
                                      gender = s.gender,
                                      address = s.address,
                                      phone = s.phoneNumber,
                                      salary = s.salary
                                  }).FirstOrDefault();
            return Json(accountDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditProfile(int id,string name,string email,string phone,string address,DateTime birthdate,bool gender)
        {
            using (var db = new AppDbContext())
            {
                Account account = db.Accounts.FirstOrDefault(a => a.accountID == id);
                monamedia.Models.Staff s = db.Staffs.FirstOrDefault(staff => staff.accountID == id);
                account.email = email;
                s.address = address;
                s.fullName = name;
                s.birthDate = birthdate;
                s.phoneNumber = phone;
                s.gender = gender;
                db.SaveChanges();
                return Json(new { success = true});
            }
        }
        public ActionResult ChangePassword(int id ,string oldPassword,string newPassword)
        {
            using (var db = new AppDbContext())
            {
                Account account = db.Accounts.FirstOrDefault(a => a.accountID == id);
                if(account.password==oldPassword)
                    account.password = newPassword;
                db.SaveChanges();
                return Json(new { success = true });
            }
        }
    }
}