using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace monamedia.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddImport(DateTime importDate,int total)
        {
            if (ModelState.IsValid)
            {
                AppDbContext db = new AppDbContext();
                int idAccount = Convert.ToInt32(Session["AccountID"]);
                monamedia.Models.Manager Managers = db.Managers.Where(row => row.accountID == idAccount).FirstOrDefault();
                monamedia.Models.Import newimport = new monamedia.Models.Import
                {
                    importDate = importDate,
                    status = "Chờ nhập",
                    managerID = Managers.managerID,
                    total =total,
                };
                db.Imports.Add(newimport);
                db.SaveChanges();
                int importID = newimport.importID;
                return Json(new { success = true, importID,message = "Thêm phiếu nhập thành công" });
            }
            return Json(new { success = false, message = "Nhập lại thông tin" });
        }
        [HttpPost]
        public ActionResult AddProductImport(int importID, string productID, int price, int priceImport, int quantity)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.ProductImport pi = new ProductImport
            {
                importID = importID,
                productID = productID,
                price = price,
                priceImport = priceImport,
                quantity = quantity,
            };
            db.ProductImports.Add(pi);
            db.SaveChanges();
            return Json(new { success = true });

        }
        public ActionResult GetProductImport(int importid)
        {
            AppDbContext db = new AppDbContext();
            var products = (from i in db.ProductImports
                            join p in db.Products
                            on i.productID equals p.productID
                            where i.importID == importid
                            select new
                            {
                                PriceImport = i.priceImport,
                                ProductID = i.productID,
                                Price = i.price,
                                Quantity = i.quantity,
                                Name = p.name,
                                Brand = p.brand,
                                Color = p.color,
                                Type = p.type,
                            }).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
    
}