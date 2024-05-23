using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monamedia.ViewModels;

namespace monamedia.Areas.Staff.Controllers
{
    public class SImportController : Controller
    {
        public class ImportDetailInfo
        {
            public string ProductImage { get; set; }
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public int? PriceImport { get; set; }
            public int? Price { get; set; }
            public int? Quantity { get; set; }
        }
        public ActionResult AllImport()
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<ImportViewModel> li = new List<ImportViewModel>();
                foreach (var i in db.Imports)
                {
                    if (i.status == "Chờ nhập")
                    {
                        ImportViewModel importViewModel = new ImportViewModel();
                        importViewModel.ImportID = i.importID;
                        importViewModel.ManagerID = i.managerID;
                        importViewModel.NameManager = db.Managers.FirstOrDefault(m => m.managerID == i.managerID).fullName;
                        importViewModel.ImportDate = i.importDate;
                        importViewModel.Total = i.total;
                        li.Add(importViewModel);
                    }
                }
                return View(li);
            }
        }
        public ActionResult GetImportDetails(int importID)
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<ImportDetailInfo> importDetailInfo = new List<ImportDetailInfo>();
                List<ProductImport> li = new List<ProductImport>();
                foreach (var i in db.ProductImports)
                {
                    if (i.importID == importID)
                        li.Add(i);
                }
                foreach (var i in li)
                {
                    ImportDetailInfo idI = new ImportDetailInfo();
                    idI.ProductImage = db.Products.FirstOrDefault(p => p.productID == i.productID).img;
                    idI.ProductName = db.Products.FirstOrDefault(p => p.productID == i.productID).name;
                    idI.ProductColor = db.Products.FirstOrDefault(p => p.productID == i.productID).color;
                    idI.PriceImport = i.priceImport;
                    idI.Price = i.price;
                    idI.Quantity = i.quantity;
                    importDetailInfo.Add(idI);
                }
                return Json(importDetailInfo, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ConfirmImport(int importID)
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<ProductImport> li = db.ProductImports.Where(p => p.importID == importID).ToList();
                foreach (var i in li)
                {
                    Product pro = db.Products.FirstOrDefault(p => p.productID == i.productID);
                    pro.quantity += i.quantity;
                    pro.price = i.price;
                }
                db.Imports.FirstOrDefault(i => i.importID == importID).status = "Đã nhập";
                db.SaveChanges();
                return Json(new { success = true, message = "Nhap hang thanh cong" });
            }
        }
    }
}