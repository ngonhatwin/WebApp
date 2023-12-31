﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp.Pages
{
    
    public class RegisterModel : PageModel
    {
        public class User
        {
            

            public string Ho { get; set; }
            public string Ten { get; set; }
            public string email { get; set; }
            public string password { get; set; }

            
        }

        [BindProperty]
        public User user { set ; get; }


        public void OnGet()
        {
            // Xử lý yêu cầu HTTP GET
        }

        public async Task<IActionResult> OnPostAsync()
        {
  
            // Xử lý yêu cầu HTTP POST
            using (var httpClient = new HttpClient())
            {
                var apiEndpoint = "https://localhost:7253/api/Account";

                User abc = user;
                var jsonContent = new StringContent(JsonSerializer.Serialize(user) ,Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"đăng kí thành công");
                    // Đăng ký thành công, thực hiện các tác vụ khác nếu cần
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Xử lý lỗi từ API
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Đăng ký không thành công: {errorMessage}");
                    return Page();
                }
            }
        }
    }
}