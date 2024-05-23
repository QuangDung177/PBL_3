using monamedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.ViewModels
{
    public class ExportController : Controller
    {
        public ActionResult ConfirmExport(int orderId)
        {
            AppDbContext db = new AppDbContext();
            int accountID = Convert.ToInt32(Session["AccountID"]);
            List<orderDetail> li = db.orderDetails.Where(o => o.orderID == orderId).ToList();
            foreach (var detail in li)
            {
                Product product = db.Products.FirstOrDefault(p => p.productID == detail.productID);
                if (product != null)
                    product.quantity -= detail.quantity;
            }
            db.C_Order.FirstOrDefault(o => o.orderID == orderId).status = "Chờ giao hàng";
            var newExport = new Export
            {
                staffID = null,
                managerID = db.Managers.FirstOrDefault(m => m.accountID == accountID).managerID,
                orderID = orderId,
                pickUpTime = null,
                status = "Chờ giao hàng"
            };
            db.Exports.Add(newExport);
            db.SaveChanges();
            return Json(new { success = true, });
        }
    }
}