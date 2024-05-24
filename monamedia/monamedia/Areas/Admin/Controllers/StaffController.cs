using monamedia.Models;
using monamedia.ViewModels;
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
        public ActionResult GetStaffExport(DateTime startDate, DateTime endDate)
        {
            AppDbContext db = new AppDbContext();
            var staffExports = (from e in db.Exports
                                join s in db.Staffs on e.staffID equals s.staffID
                                join o in db.C_Order on e.orderID equals o.orderID
                                where e.pickUpTime >= startDate && e.pickUpTime <= endDate && e.status == "Giao thành công"
                                group o by new { s.staffID, s.fullName } into g
                                select new StaffExportViewModel
                                {
                                    StaffID = g.Key.staffID,
                                    FullName = g.Key.fullName,
                                    DeliveredQuantity = g.Count(),
                                    TotalRevenue = g.Sum(x => x.total) != null ? g.Sum(x => x.total) : 0
                                }).ToList();

            return Json(staffExports, JsonRequestBehavior.AllowGet);
        }
    }
}