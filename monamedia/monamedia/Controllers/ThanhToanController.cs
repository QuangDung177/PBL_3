using monamedia.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using monamedia.Models;
namespace monamedia.Controllers
{
    public class ThanhToanController : Controller
    {
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);

        }
        public ActionResult ThanhToan()
        {
            return View();
        }
        public async Task<ActionResult> Indexs(string masp)
        {
            try
            {
               
                var loginService = new LoginService();
                string result = await loginService.ShopingCartDeatail(masp);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var productsArray = jsonResponse["document"]["ResultTable1"];
                    var ShopingCartDeatail = productsArray.Select(p => new GiohangViewModel
                    {
                        ProductId = p["Productid"].Value<string>(),
                        Name = p["Name"].Value<string>(),
                        Brand = p["Brand"].Value<string>(),
                        Price = p["Price"].Value<int>(),
                        Quantity = p["Quantity"].Value<int>(),
                        Color = p["Color"].Value<string>(),
                        Ram = p["Ram"].Value<string>(),
                        Memory = p["Memory"].Value<string>(),
                        SpecId = p["Specid"].Value<string>(),
                        Img = p["Img"].Value<string>(),
                        Type = p["Type"].Value<string>(),
                        SpecId1 = p["Specid1"].Value<string>(),
                        Cpu = p["Cpu"].Value<string>(),
                        Gpu = p["Gpu"].Value<string>(),
                        Os = p["Os"].Value<string>(),
                        Screen = p["Screen"].Value<string>(),
                        Pin = p["Pin"].Value<string>(),
                        Camera = p["Camera"].Value<string>(),
                        Size = p["Size"].Value<string>(),
                        Warranty = p["Warrantly"].Value<int>(),
                        Sound = p["Sound"].Value<string>(),
                        Weight = p["Weight"].Value<string>(),
                        ConnectivityTechnologies = p["Conectivitytechnologies"].Value<string>(),
                        Charge = p["Charge"].Value<string>(),
                        YearOfDebut = p["Yearofdebut"].Value<int>()
                    }).ToList();

                    ViewBag.ShopingCartDeatail = ShopingCartDeatail;
                }
                else
                {
                    ViewBag.ErrorMessage = "Lỗi khi tải sản phẩm từ API: " + message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thực hiện tải sản phẩm từ API. Vui lòng thử lại sau.";
            }

            return View();
        }
        public async Task<ActionResult> ChiTiet(string masp)
        {
            try
            {

                var loginService = new LoginService();
                string result = await loginService.ShopingCartDeatail(masp);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var productsArray = jsonResponse["document"]["ResultTable1"];
                    var ShopingCartDeatail = productsArray.Select(p => new GiohangViewModel
                    {
                        ProductId = p["Productid"].Value<string>(),
                        Name = p["Name"].Value<string>(),
                        Brand = p["Brand"].Value<string>(),
                        Price = p["Price"].Value<int>(),
                        Quantity = p["Quantity"].Value<int>(),
                        Color = p["Color"].Value<string>(),
                        Ram = p["Ram"].Value<string>(),
                        Memory = p["Memory"].Value<string>(),
                        SpecId = p["Specid"].Value<string>(),
                        Img = p["Img"].Value<string>(),
                        Type = p["Type"].Value<string>(),
                        SpecId1 = p["Specid1"].Value<string>(),
                        Cpu = p["Cpu"].Value<string>(),
                        Gpu = p["Gpu"].Value<string>(),
                        Os = p["Os"].Value<string>(),
                        Screen = p["Screen"].Value<string>(),
                        Pin = p["Pin"].Value<string>(),
                        Camera = p["Camera"].Value<string>(),
                        Size = p["Size"].Value<string>(),
                        Warranty = p["Warrantly"].Value<int>(),
                        Sound = p["Sound"].Value<string>(),
                        Weight = p["Weight"].Value<string>(),
                        ConnectivityTechnologies = p["Conectivitytechnologies"].Value<string>(),
                        Charge = p["Charge"].Value<string>(),
                        YearOfDebut = p["Yearofdebut"].Value<int>()
                    }).ToList();

                    ViewBag.ChiTiet = ShopingCartDeatail;
                }
                else
                {
                    ViewBag.ErrorMessage = "Lỗi khi tải sản phẩm từ API: " + message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thực hiện tải sản phẩm từ API. Vui lòng thử lại sau.";
            }

            return View();
        }
        Models.AppDbContext db = new Models.AppDbContext();

        public ActionResult ThemVaoGio(string SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Models.Product sp = db.Products.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem newItem = new CartItem()
                {
                    SanPhamID = SanPhamID,
                    TenSanPham = sp.productID,
                    SoLuong = 1,
                    Hinh = sp.img,
                    DonGia = sp.price.ToString(),

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
                
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult XoaKhoiGio(string SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID.Equals(SanPhamID));
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }



        public async Task<ActionResult> ShopingCart(OrderModel model)
        {
            try
            {
                if (model.CustomerID == 0)
                {
                    var customerInfo = (CustomerInfo)Session["CustomerInfo"];
                    model.CustomerID = customerInfo.customerID;
                }

                List<CartItem> giohang = Session["giohang"] as List<CartItem>;

                if (giohang == null || giohang.Count == 0)
                {
                    ViewBag.ErrorMessage = "Không có sản phẩm trong giỏ hàng.";
                    return View("Error");
                }


                foreach (var product in giohang)
                {
                    var result = await ShopingCartAPI(model.CustomerID, model.Fee, product.ThanhTien, product.SanPhamID, product.SoLuong);
                    if (result != "Success")
                    {
                        ViewBag.ErrorMessage = "Đã xảy ra lỗi khi đặt hàng cho sản phẩm: " + product.SanPhamID;
                        return View("Error");
                    }
                }

                Session.Remove("ProductsInCart");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi đặt hàng: " + ex.Message;
                return View("Error");
            }
        }
        private async Task<string> ShopingCartAPI(int customerID, int fee, int total, string productID, int quantity)
        {
            try
            {
                var loginService = new LoginService();
                // Gửi dữ liệu đặt hàng đến API
                var result = await loginService.ShopingCart(customerID, fee, total, productID, quantity);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();
                if (code == 1 && message == "SP Executed Successfully")
                {
                    return "Success";
                }
                else
                {
                    return "Failure";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi gửi dữ liệu đặt hàng đến API: " + ex.Message);
            }
        }


    }
}
