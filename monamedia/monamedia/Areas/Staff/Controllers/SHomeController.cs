using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monamedia.Models;
namespace monamedia.Areas.Staff.Controllers
{
    public class SHomeController : Controller
    {
        public ActionResult AllProduct()
        {
            AppDbContext db = new AppDbContext();
            int accountID = Convert.ToInt32(Session["AccountID"]);
            Models.Staff s = db.Staffs.FirstOrDefault(staff=>staff.accountID==accountID);
            List<Product> products = db.Products.ToList();
            List<string> specIDs = new List<string>();
            foreach (var i in db.Specifications)
                specIDs.Add(i.SpecID);
            ViewBag.SpecIDs = specIDs;
            ViewBag.NameStaff = s.fullName;
            return View(products);
        }
        
    }
}