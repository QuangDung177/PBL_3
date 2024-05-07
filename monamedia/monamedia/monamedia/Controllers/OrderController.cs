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

    }
}