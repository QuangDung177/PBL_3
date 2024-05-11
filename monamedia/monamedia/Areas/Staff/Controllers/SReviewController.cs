using monamedia.Models;
using monamedia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Areas.Staff.Controllers
{
    public class SReviewController : Controller
    {
        // GET: Staff/SReview
        public ActionResult AllReview()
        {
            AppDbContext db=new AppDbContext();
            List<ReviewViewModel> list = new List<ReviewViewModel>();
            foreach(var i in db.Reviews)
            {
                if(i.cmtStaff==null)
                {
                    var orderDetail = db.orderDetails.FirstOrDefault(od => od.orderDetailID == i.orderDetailID);
                    var order = db.C_Order.FirstOrDefault(o => o.orderID == orderDetail.orderID);
                    var customer = db.Customers.FirstOrDefault(c => c.customerID == order.customerID);
                    var product = db.Products.FirstOrDefault(p => p.productID == orderDetail.productID);
                    ReviewViewModel review = new ReviewViewModel();
                    review.reviewID = i.reviewID;
                    review.OrderDetailId = i.orderDetailID;
                    review.NameCustomer = customer.fullName;
                    review.NameProduct = product.name;
                    review.ImageUrl = product.img;
                    review.ProductColor = product.color;
                    review.Star = i.star;
                    review.CmtCustomer = i.cmtCustomer;
                    review.CmtStaff = null;
                    review.CmtTime = i.cmtTime;
                    list.Add(review);
                }
            }
            return View(list);
        }
        public ActionResult AllReviewed()
        {
            AppDbContext db = new AppDbContext();
            List<ReviewViewModel> list = new List<ReviewViewModel>();
            foreach (var i in db.Reviews)
            {
                if (i.cmtStaff !=null)
                {
                    var orderDetail = db.orderDetails.FirstOrDefault(od => od.orderDetailID == i.orderDetailID);
                    var order = db.C_Order.FirstOrDefault(o => o.orderID == orderDetail.orderID);
                    var customer = db.Customers.FirstOrDefault(c => c.customerID == order.customerID);
                    var product = db.Products.FirstOrDefault(p => p.productID == orderDetail.productID);
                    ReviewViewModel review = new ReviewViewModel();
                    review.reviewID = i.reviewID;
                    review.OrderDetailId = i.orderDetailID;
                    review.NameCustomer = customer.fullName;
                    review.NameProduct = product.name;
                    review.ImageUrl = product.img;
                    review.ProductColor = product.color;
                    review.Star = i.star;
                    review.CmtCustomer = i.cmtCustomer;
                    review.CmtStaff = i.cmtStaff;
                    review.CmtTime = i.cmtTime;
                    list.Add(review);
                }
            }
            return View(list);
        }
        public ActionResult ReplyReview(int reviewID,string cmtStaff)
        {
                AppDbContext db = new AppDbContext();
                Review review = db.Reviews.FirstOrDefault(r => r.reviewID == reviewID);
                review.cmtStaff = cmtStaff;
                db.SaveChanges();
                return Json(new { success = true });
        }
    }
}