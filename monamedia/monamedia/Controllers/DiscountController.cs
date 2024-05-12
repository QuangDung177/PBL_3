using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Discount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDiscount(monamedia.ViewModels.DiscountViewModel model)
        {
            if (ModelState.IsValid)
            {

                // Tạo một bản ghi mới trong bảng Discount
                AppDbContext db = new AppDbContext();
                Discount newDiscount = new Discount
                {
                    nameDiscount = model.Name,
                    per_cent = model.Percent,
                    ngayBatDau = model.DateStart,
                    ngayKetThuc = model.DateEnd
                };
                db.Discounts.Add(newDiscount);
                db.SaveChanges();
                List<monamedia.Models.Product> listproduct = new List<monamedia.Models.Product>(); ;
                foreach (var productId in model.SelectedProductIds)
                {
                    var products = db.Products.Where(row => row.productID == productId).ToList();
                    listproduct.AddRange(products);
                }
                foreach (Product p in listproduct)
                {
                    newDiscount.Products.Add(p);
                }
                db.SaveChanges();

                return Json(new { success = true, message = "Thêm giảm giá thành công" });
            }

            // Trong trường hợp ModelState không hợp lệ, bạn có thể xử lý lỗi ở đây

            return Json(new { success = false, message = "Nhập lại thông tin" });
        }
        public ActionResult GetAllBrands()
        {
            AppDbContext db = new AppDbContext();
            List<string> brands = db.Products.Select(p => p.brand).Distinct().ToList();
            return Json(brands, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductTypesByBrand(string brand)
        {
            AppDbContext db = new AppDbContext();
            List<string> type = db.Products.Where(p => p.brand == brand).Select(p => p.type).Distinct().ToList();
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDiscountProducts(int discountId)
        {
            AppDbContext db = new AppDbContext();
            var products = db.Discounts
                            .Include("Products")
                            .Where(d => d.discountID == discountId)
                            .SelectMany(d => d.Products)
                            .Select(p => new
                            {
                                ProductID = p.productID,
                                ProductName = p.name,
                                Color = p.color,
                                Brand = p.brand,
                                ProductType = p.type,
                            })
                            .ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

    }
}