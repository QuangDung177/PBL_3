using monamedia.Models;
using monamedia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProductDetails(string productId)
        {
            AppDbContext db = new AppDbContext();
            var productDetails = (from p in db.Products
                                  join s in db.Specifications on p.specID equals s.SpecID
                                  where p.productID.ToString() == productId
                                  select new
                                  {
                                      ProductID = p.productID,
                                      ProductName = p.name,
                                      ProductBrand = p.brand,
                                      ProductPrice = p.price,
                                      ProductQuantity = p.quantity,
                                      ProductColor = p.color,
                                      ProductRam = p.ram,
                                      ProductMemory = p.memory,
                                      CPU = s.CPU,
                                      GPU = s.GPU,
                                      OS = s.OS,
                                      Screen = s.screen,
                                      Pin = s.pin,
                                      Camera = s.camera,
                                      Size = s.size,
                                      Warrantly = s.warrantly,
                                      Sound = s.sound,
                                      Weight = s.weight,
                                      CT = s.conectivityTechnologies,
                                      Charge = s.charge,
                                      Year = s.yearOfDebut
                                  }).FirstOrDefault();

            return Json(productDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var product = db.Products.FirstOrDefault(p => p.productID == model.ProductID);
                    if (product != null)
                    {
                        product.name = model.Name;
                        product.ram = model.Ram;
                        product.memory = model.Memory;
                        product.quantity = model.Quantity;
                        product.price = model.Price;
                        product.color = model.Color;
                        var spec = db.Specifications.FirstOrDefault(s => s.SpecID == product.specID);
                        if (spec != null)
                        {
                            spec.CPU = model.CPU;
                            spec.GPU = model.GPU;
                            spec.OS = model.OS;
                            spec.screen = model.Screen;
                            spec.pin = model.Pin;
                            spec.charge = model.Charge;
                            spec.camera = model.Camera;
                            spec.size = model.Size;
                            spec.warrantly = model.Warrantly;
                            spec.sound = model.Sound;
                            spec.weight = model.Weight;
                            spec.conectivityTechnologies = model.CT;
                            spec.yearOfDebut = model.Year;
                        }
                    }
                    db.SaveChanges();
                }
                return Json(new { success = true, message = "Cập nhật sản phẩm thành công." });
            }
            else
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }
        }
        [HttpPost]
        public ActionResult AddProductWithNewSpec(Product product, Specification spec)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Specifications.Add(spec);
                db.SaveChanges();
                product.specID = spec.SpecID;
                db.Products.Add(product);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Thêm sản phẩm thành công." });
        }

        [HttpPost]
        public ActionResult AddProductWithExistedSpec(Product product, string specID)
        {
            using (AppDbContext db = new AppDbContext())
            {
                product.specID = specID;
                db.Products.Add(product);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Thêm sản phẩm thành công." });
        }
        public ActionResult Getsoldproduct(DateTime startDate, DateTime endDate)
        {
            AppDbContext db = new AppDbContext(); 
            var soldProducts = (from od in db.orderDetails
                                join o in db.C_Order on od.orderID equals o.orderID
                                join p in db.Products on od.productID equals p.productID
                                where (o.timeOrder >= startDate && o.timeOrder <= endDate&& o.status == "Giao thành công")
                                group od by new { od.productID, p.name, p.brand, p.color, p.type } into g
                                select new SoldProductViewModel
                                {
                                    ProductID = g.Key.productID,
                                    Name = g.Key.name,
                                    Brand = g.Key.brand,
                                    Color = g.Key.color,
                                    Type = g.Key.type,
                                    QuantitySold = g.Sum(x => x.quantity) != null ? g.Sum(x => x.quantity) : 0
                                }).ToList();

            return Json(soldProducts, JsonRequestBehavior.AllowGet);
        }
    }


}
