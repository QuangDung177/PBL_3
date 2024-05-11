using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Admin/Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetStaffDetails(string accountid)
        {
            AppDbContext db = new AppDbContext();
            var StaffDetails = (from a in db.Accounts
                                join s in db.Staffs
                               on a.accountID equals s.accountID
                                where a.accountID.ToString() == accountid
                                select new
                                {
                                    UserName = a.userName,
                                    Email = a.email,
                                    Password = a.password,
                                    FullName = s.fullName,
                                    BirthDate = s.birthDate,
                                    Gender = s.gender,
                                    Address = s.address,
                                    PhoneNumber = s.phoneNumber,
                                    Salary = s.salary,
                                }).FirstOrDefault();
            return Json(StaffDetails, JsonRequestBehavior.AllowGet);
        }
    }
}