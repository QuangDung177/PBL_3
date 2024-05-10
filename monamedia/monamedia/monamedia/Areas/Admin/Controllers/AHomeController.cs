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
        public ActionResult Index(string search = "")
        {
            AppDbContext db = new AppDbContext();
            List<monamedia.Models.Staff> Staffs = db.Staffs.Where(row => row.fullName.Contains(search)).ToList();
            ViewBag.Search = search;
            Filter();
            return View(Staffs);
        }
        public void Filter()
        {
            AppDbContext db = new AppDbContext();
            int idAccount = Convert.ToInt32(Session["AccountID"]);
            monamedia.Models.Manager Managers = db.Managers.Where(row => row.accountID == idAccount).FirstOrDefault();
            ViewBag.ManagerName = Managers.fullName;
        }
        public ActionResult Information()
        {
            int idAccount = Convert.ToInt32(Session["AccountID"]);
            AppDbContext db = new AppDbContext();
            monamedia.Models.Manager Managers = db.Managers.Where(row => row.accountID == idAccount).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == idAccount).FirstOrDefault();
            var viewModel = new monamedia.Areas.Admin.Data.EditInformation
            {
                Manager = Managers,
                Account = Accounts
            };
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ManagerName = Managers.fullName;
            ViewBag.accountID = idAccount;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Information(monamedia.Models.Manager m, monamedia.Models.Account a)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.Manager Managers = db.Managers.Where(row => row.managerID == m.managerID).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == a.accountID).FirstOrDefault();
            //Update
            //account
            Accounts.userName = a.userName;
            Accounts.email = a.email;
            Accounts.role = a.role;
            //Staff
            Managers.phoneNumber = m.phoneNumber;
            Managers.address = m.address;
            Managers.birthDate = m.birthDate;
            Managers.experience = m.experience;
            Managers.gender = m.gender;
            Managers.fullName = m.fullName;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Thông tin đã được cập nhật thành công!";
            return RedirectToAction("Information");
        }
        [HttpPost]
        public ActionResult ChangePassword(string password, int accountid)
        {
        
            AppDbContext db = new AppDbContext();
            monamedia.Models.Manager Managers = db.Managers.Where(row => row.accountID == accountid).FirstOrDefault();
            monamedia.Models.Account account = db.Accounts.FirstOrDefault(a => a.accountID == accountid);
            if (account != null)
            {
                account.password = password;
                db.SaveChanges();
                var viewModel = new monamedia.Areas.Admin.Data.EditInformation
                {
                 Manager = db.Managers.Where(row => row.accountID == accountid).FirstOrDefault(),
                Account = account
                };
                return Json(new { success = true, message = "Đổi mật khẩu thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Đổi mật khẩu thất bại! Không tìm thấy tài khoản." });
            }
        }
        public ActionResult AccountManage(string search = "", string filter = "")
        {
            AppDbContext db = new AppDbContext();
            List<monamedia.Models.Account> Accounts;

            switch (filter)
            {
                case "guest":
                    // Tìm kiếm trong danh sách khách hàng
                    Accounts = db.Accounts.Where(row => row.userName.Contains(search) && row.role == "Guest").ToList();
                    break;
                case "staff":
                    // Tìm kiếm trong danh sách nhân viên
                    Accounts = db.Accounts.Where(row => row.userName.Contains(search) && row.role == "Staff").ToList();
                    break;
                default:
                    // Tìm kiếm trong toàn bộ danh sách
                    Accounts = db.Accounts.Where(row => row.userName.Contains(search)).ToList();
                    break;
            }
            Filter();
            ViewBag.Search = search;
            return View(Accounts);
        }
        public ActionResult CreateStaff()
        {
            Filter();
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(monamedia.Models.Staff s, monamedia.Models.Account a)
        {
            AppDbContext db = new AppDbContext();
            db.Accounts.Add(a);
            db.SaveChanges();
            s.accountID = a.accountID;
            db.Staffs.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditStaff(int idStaff, int idAccount)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.Staff Staffs = db.Staffs.Where(row => row.staffID == idStaff).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == idAccount).FirstOrDefault();
            var viewModel = new monamedia.Areas.Admin.Data.EditStaffViewModel
            {
                Staff = Staffs,
                Account = Accounts
            };
            Filter();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditStaff(monamedia.Models.Staff s, monamedia.Models.Account a)
        {
            AppDbContext db = new AppDbContext();
            monamedia.Models.Staff Staffs = db.Staffs.Where(row => row.staffID == s.staffID).FirstOrDefault();
            monamedia.Models.Account Accounts = db.Accounts.Where(row => row.accountID == a.accountID).FirstOrDefault();
            //Update
            //account
            Accounts.userName = a.userName;
            Accounts.password = a.password;
            Accounts.email = a.email;
            Accounts.role = a.role;
            //Staff
            Staffs.phoneNumber = s.phoneNumber;
            Staffs.address = s.address;
            Staffs.birthDate = s.birthDate;
            Staffs.salary = s.salary;
            Staffs.gender = s.gender;
            Staffs.fullName = s.fullName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteStaff(string selectedStaffIds, string selectedAccountIds)
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