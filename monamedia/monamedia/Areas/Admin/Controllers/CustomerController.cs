using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCustomerDetails(string accountid)
        {
            AppDbContext db = new AppDbContext();
            var CustomerDetails = (from a in db.Accounts
                                   join c in db.Customers
                                  on a.accountID equals c.accountID
                                   where a.accountID.ToString() == accountid
                                   select new
                                   {
                                       UserName = a.userName,
                                       Email = a.email,
                                       Password = a.password,
                                       FullName = c.fullName,
                                       BirthDate = c.birthDate,
                                       Gender = c.gender,
                                       Address = c.address,
                                       PhoneNumber = c.phoneNumber,
                                       AccountStatus = c.accountStatus,
                                   }).FirstOrDefault();
            return Json(CustomerDetails, JsonRequestBehavior.AllowGet);
        }
    }
}