﻿using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace monamedia.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public class OrderDetailInfo
        {
            public string ProductImage { get; set; }
            public string ProductName { get; set; }
            public int? Quantity { get; set; }
            public int? Price { get; set; }
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            AppDbContext db = new AppDbContext();
            List<OrderDetailInfo> orderDetails = new List<OrderDetailInfo>();
            List<orderDetail> li = new List<orderDetail>();
            foreach (var i in db.orderDetails)
            {
                if (i.orderID == orderId)
                    li.Add(i);
            }
            foreach (var detail in li)
            {
                var productInfo = db.Products.FirstOrDefault(p => p.productID == detail.productID);
                OrderDetailInfo detailInfo = new OrderDetailInfo
                {
                    ProductImage = productInfo.img,
                    ProductName = productInfo.name,
                    Quantity = detail.quantity,
                    Price = detail.quantity * productInfo.price
                };
                orderDetails.Add(detailInfo);
            }

            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteOrder(int? orderId)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    C_Order order = new C_Order();
                    foreach(var i in db.C_Order)
                    {
                        if (i.orderID == orderId)
                            order = i;
                    }
                    if (order == null)
                    {
                        return HttpNotFound(); 
                    }
                    List<orderDetail> orderDetails = db.orderDetails.Where(od => od.orderID == orderId).ToList();

                    db.orderDetails.RemoveRange(orderDetails);
                    db.C_Order.Remove(order);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Xoa thanh cong" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Xoa that bai " });
            }
        }
        public ActionResult ConfirmOrder(int? orderId)
        {
            int accountId = Convert.ToInt32(Session["AccountID"]);
            AppDbContext db= new AppDbContext();
            Staff s=db.Staffs.FirstOrDefault(staff=>staff.accountID== accountId);
            C_Order order=db.C_Order.FirstOrDefault(o=>o.orderID == orderId);
            order.status = "Đã xác nhận";
            order.staffID = s.staffID;
            db.SaveChanges();
            return Json(new { success = true, });
        }
        public ActionResult ReceiveDelivery(int? orderId)
        {
            int accountId = Convert.ToInt32(Session["AccountID"]);
            AppDbContext db = new AppDbContext();
            Staff s = db.Staffs.FirstOrDefault(staff => staff.accountID == accountId);
            C_Order order = db.C_Order.FirstOrDefault(o => o.orderID == orderId);
            var export = new Export
            {
                staffID = s.staffID,
                managerID = 1,
                orderID = orderId,
                pickUpTime = DateTime.Now,
                status = "Đang giao hàng"
            };
            db.Exports.Add(export);
            order.status = "Chờ giao hàng";
            db.SaveChanges();
            return Json(new { success = true, });
        }
        public ActionResult DeliveryFailure(int ? orderId)
        {
            AppDbContext db = new AppDbContext();
            C_Order order = db.C_Order.FirstOrDefault(o => o.orderID == orderId);
            Export export=db.Exports.FirstOrDefault(e=>e.orderID == orderId);
            order.status = "Đã xác nhận";
            db.Exports.Remove(export);
            db.SaveChanges();
            return Json(new { success = true, });
        }
        public ActionResult DeliverySuccess(int? orderId)
        {
            AppDbContext db = new AppDbContext();
            C_Order order = db.C_Order.FirstOrDefault(o => o.orderID == orderId);
            Export export = db.Exports.FirstOrDefault(e => e.orderID == orderId);
            order.status = "Giao thành công";
            export.status= "Giao thành công";
            db.SaveChanges();
            return Json(new { success = true, });
        }
    }
}