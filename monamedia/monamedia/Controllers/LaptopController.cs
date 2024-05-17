using monamedia.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static monamedia.API.SanPham;

namespace monamedia.Controllers
{
    public class LaptopController : Controller
    {
        public async Task<ActionResult> Index()
        {
            await Indexs();
            return View();
        }
        public async Task<ActionResult> Indexs()
        {
            try
            {
                string masp = "Laptop"; // Tên sản phẩm cần hiển thị
                var loginService = new LoginService();
                string result = await loginService.LoadPro(masp);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var productsArray = jsonResponse["document"]["ResultTable1"];
                    var laptops = productsArray.Select(p => new LaptopViewModel
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

                    ViewBag.Laptops = laptops;
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

        public async Task<ActionResult> Phone()
        {
            try
            {
                string masp = "Phone"; // Tên sản phẩm cần hiển thị
                var loginService = new LoginService();
                string result = await loginService.LoadPro(masp);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var productsArray = jsonResponse["document"]["ResultTable1"];
                    var laptops = productsArray.Select(p => new LaptopViewModel
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

                    ViewBag.Phone = laptops;
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

        public async Task<ActionResult> Tablet()
        {
            try
            {
                string masp = "Tablet"; // Tên sản phẩm cần hiển thị
                var loginService = new LoginService();
                string result = await loginService.LoadPro(masp);

                var jsonResponse = JObject.Parse(result);
                int code = jsonResponse["code"].Value<int>();
                string message = jsonResponse["message"].Value<string>();

                if (code == 1 && message == "SP Executed Successfully")
                {
                    var productsArray = jsonResponse["document"]["ResultTable1"];
                    var laptops = productsArray.Select(p => new LaptopViewModel
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

                    ViewBag.Tablet = laptops;
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

    }
}