using monamedia.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace monamedia.Areas.Admin.Controllers
{
    
    public class AHomeController : Controller
    {
        // GET: Admin/AHome
        public ActionResult Index(string search="")
        {
            AppDbContext db = new AppDbContext();
            List<monamedia.Models.Staff> Staffs=db.Staffs.Where(row => row.fullName.Contains(search)).ToList();
            ViewBag.Search= search;
            return View(Staffs);
        }
        public ActionResult CreateStaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(monamedia.Models.Staff s,monamedia.Models.Account a) {
            AppDbContext db = new AppDbContext();
            db.Accounts.Add(a);
            db.SaveChanges();
            s.accountID= a.accountID;
            db.Staffs.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditStaff(int idStaff,int idAccount)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.Staff Staffs = db.Staffs.Where(row => row.staffID == idStaff).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == idAccount).FirstOrDefault();
            var viewModel = new monamedia.Areas.Admin.Data.EditStaffViewModel
            {
                Staff = Staffs,
                Account = Accounts
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditStaff(monamedia.Models.Staff s,monamedia.Models.Account a)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.Staff Staffs = db.Staffs.Where(row => row.staffID == s.staffID).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == a.accountID).FirstOrDefault();
           //Update
           //account
           Accounts.userName= a.userName;
            Accounts.password= a.password;
            Accounts.email= a.email;
            Accounts.role= a.role;
            //Staff
            Staffs.phoneNumber=s.phoneNumber;
            Staffs.address=s.address;
            Staffs.birthDate=s.birthDate;
            Staffs.salary=s.salary;
            Staffs.gender=s.gender;
            Staffs.fullName=s.fullName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteStaff(string selectedStaffIds,string selectedAccountIds)
        {
            AppDbContext db = new AppDbContext();
            var Staffids = selectedStaffIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToList();

            var Accountids = selectedAccountIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToList();
            foreach (int accountId in Accountids)
            {
                // Kiểm tra xem có nhân viên nào tham chiếu đến tài khoản này không
                var associatedStaffs = db.Staffs.Where(s => s.accountID == accountId).ToList();
                if (associatedStaffs.Count > 0)
                {
                    // Xử lý các tham chiếu của nhân viên đến tài khoản trước khi xóa tài khoản
                    foreach (var staff in associatedStaffs)
                    {
                        staff.accountID = null; // hoặc thực hiện các hành động khác tùy theo yêu cầu của ứng dụng của bạn
                    }
                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
            }
            foreach (int id in Accountids)
            {
                monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == id).FirstOrDefault();
                db.Accounts.Remove(Accounts);
                db.SaveChanges();
            }
            foreach (int id in Staffids)
            {
                monamedia.Models.Staff Staffs = db.Staffs.Where(row => row.staffID == id).FirstOrDefault();
                db.Staffs.Remove(Staffs);
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }



    }
}