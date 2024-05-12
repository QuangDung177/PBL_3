﻿using monamedia.Models;
using monamedia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Areas.Staff.Controllers
{
    public class SDeliveryController : Controller
    {
        public ActionResult AllDelivery()
        {
            AppDbContext db = new AppDbContext();
            List<OrderViewModel> li = new List<OrderViewModel>();
            foreach (var i in db.C_Order)
            {
                if (i.status == "Chờ giao hàng")
                {
                    Customer customer = db.Customers.Find(i.customerID);
                    OrderViewModel orderViewModel = new OrderViewModel();
                    orderViewModel.OrderID = i.orderID;
                    orderViewModel.StaffID = i.staffID;
                    orderViewModel.NameCustomer = customer.fullName;
                    orderViewModel.Address = customer.address;
                    orderViewModel.Phone = customer.phoneNumber;
                    orderViewModel.TimeOrder = i.timeOrder;
                    orderViewModel.Method = i.method;
                    orderViewModel.fee = i.fee;
                    orderViewModel.total = i.total;
                    li.Add(orderViewModel);
                }
            }
            return View(li);
        }
        public ActionResult AllDelivering()
        {
            AppDbContext db = new AppDbContext();
            List<DeliveringViewModel> li = new List<DeliveringViewModel>();
            foreach (var i in db.C_Order)
            {
                if (i.status == "Đang giao hàng")
                {
                    Customer customer = db.Customers.Find(i.customerID);
                    DeliveringViewModel deliveringViewModel = new DeliveringViewModel();
                    deliveringViewModel.OrderID = i.orderID;
                    deliveringViewModel.StaffID = i.staffID;
                    deliveringViewModel.NameCustomer = customer.fullName;
                    deliveringViewModel.Address = customer.address;
                    deliveringViewModel.Phone = customer.phoneNumber;
                    deliveringViewModel.TimeOrder = i.timeOrder;
                    deliveringViewModel.Method = i.method;
                    deliveringViewModel.fee = i.fee;
                    deliveringViewModel.total = i.total;
                    deliveringViewModel.pickUpTime = db.Exports.FirstOrDefault(x => x.orderID == i.orderID).pickUpTime;
                    li.Add(deliveringViewModel);
                }
            }
            return View(li);
        }
        public ActionResult AllDelivered()
        {
            AppDbContext db = new AppDbContext();
            List<DeliveringViewModel> li = new List<DeliveringViewModel>();
            foreach (var i in db.C_Order)
            {
                if (i.status == "Giao thành công")
                {
                    Customer customer = db.Customers.Find(i.customerID);
                    DeliveringViewModel deliveringViewModel = new DeliveringViewModel();
                    deliveringViewModel.OrderID = i.orderID;
                    deliveringViewModel.StaffID = i.staffID;
                    deliveringViewModel.NameCustomer = customer.fullName;
                    deliveringViewModel.Address = customer.address;
                    deliveringViewModel.Phone = customer.phoneNumber;
                    deliveringViewModel.TimeOrder = i.timeOrder;
                    deliveringViewModel.Method = i.method;
                    deliveringViewModel.fee = i.fee;
                    deliveringViewModel.total = i.total;
                    deliveringViewModel.pickUpTime = db.Exports.FirstOrDefault(x => x.orderID == i.orderID).pickUpTime;
                    li.Add(deliveringViewModel);
                }
            }
            return View(li);

        }
    }
}