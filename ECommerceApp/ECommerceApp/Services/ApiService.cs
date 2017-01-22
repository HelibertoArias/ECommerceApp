using ECommerceApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
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
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<List<T>> Get<T>(string controller) where T : class
        {
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://zulu-software.com");
                    var url = $"/Ecommerce/api/{controller}/";

                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<List<T>>(result);

                    return list;
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<Response> NewCustomer(Customer customer)
        {

            try
            {
                var request = JsonConvert.SerializeObject(customer);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://zulu-software.com");
                    var url = "/Ecommerce/api/Customers";
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = response.StatusCode.ToString() 
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var newcustomer = JsonConvert.DeserializeObject<Customer>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Cliente creado oK",
                        Result = newcustomer
                    };

                }

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        public async Task<Response> SetPhoto(int customerId, Stream stream)
        {
            try
            {
                var array = ReadFully(stream);

                var photoRequest = new PhotoRequest
                {
                    Id = customerId,
                    Array = array,
                };

                var request = JsonConvert.SerializeObject(photoRequest);
                var body = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Customers/SetPhoto";
                var response = await client.PostAsync(url, body);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Foto asignada Ok",
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<Response> UpdateCustomer(Customer customer)
        {
            try
            {
                var request = JsonConvert.SerializeObject(customer);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://zulu-software.com");
                    var url = $"/Ecommerce/api/Customers/{customer.CustomerId}";
                    var response = await client.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = response.StatusCode.ToString()
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var newcustomer = JsonConvert.DeserializeObject<Customer>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Cliente actualizado oK",
                        Result = newcustomer
                    };

                }

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
