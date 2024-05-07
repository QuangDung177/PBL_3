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
        // GET: Staff/SHome
        public ActionResult Index()
        {
           
            AppDbContext db = new AppDbContext();
            List<Product> products = db.Products.ToList();
            return View(products);
        }
        
    }
}