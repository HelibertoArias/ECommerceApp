using ECommerceApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
   public class ApiService
    {
        public async Task<Response> Login(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequest { Email = email, Password = password };

                var request = JsonConvert.SerializeObject(loginRequest);
                var content =new   StringContent(request, Encoding.UTF8, "application/json");
                using (var client= new HttpClient())
                {
                    client.BaseAddress = new Uri("http://zulu-software.com");
                    var url = "/Ecommerce/api/Users/Login";
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Usuario o contraseña incorrectos"
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Mensaje oK",
                        Result = user
                    };


                }

            }
            catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
