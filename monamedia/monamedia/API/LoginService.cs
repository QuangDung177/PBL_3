using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace monamedia.API
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44376/"); // Thay đổi thành URL của API
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJhZG1pbiIsIkN1c3RvbU9iamVjdENhbkJlQWRkZWRIZXJlIl0sIm5iZiI6MTcxNDY1NTI5NywiZXhwIjoxNzE1MjYwMDk3LCJpYXQiOjE3MTQ2NTUyOTd9.w3wfHZX3P1BpWrX9issxCjPTsd0DGNOXUAQM9mjgLAE");
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            try
            {
                var loginData = new { username = username, password = password };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("v1/api/SP/Sp_Login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu từ phản hồi thành công
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Xử lý lỗi nếu cần
                    return "Đăng nhập thất bại";
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                return "Đã xảy ra lỗi: " + ex.Message;
            }
        }
        private static Random random = new Random();
        int randomValue = random.Next(1, 9999);
        public async Task<string> RegisterAsync( string username, string password,  string email, string fullName, DateTime birthDate, int gender, string address, string phoneNumber)
        {

            try
            {
                int a= randomValue;
      
                 var registerData = new
                {
                    accountID= a,
                    username = username,
                    password = password,
                    email = email,
                    customerID=a,
                    fullName = fullName,
                    birthDate = birthDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    gender = gender,
                    address = address,
                    phoneNumber = phoneNumber,
                    accountStatus = "Guest"
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(registerData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("v1/api/SP/Sp_Reg", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return "Đăng ký thất bại";
                }
            }
            catch (Exception ex)
            {
                return "Đã xảy ra lỗi: " + ex.Message;
            }
        }

    }
}