using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Data
{
    public class DataService
    {
        public Response UpdateUser(User user)
        {
            try
            {
                using(var da = new DataAccess())
                {
                    da.Update<User>(user);
                }

                return new Response()
                {
                    IsSuccess = true,
                    Message = "Usuario actualizado Ok"
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message

                };
            }
        }

        public User GetUser()
        {
            using (var da=new DataAccess())
            {
                return da.First<User>(true);
            }
        }

        
        public Response InsertUser(User user) {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldUser = da.First<User>(false);
                    if (oldUser != null)
                    {
                        da.Delete(oldUser);
                    }

                    da.Insert(user);

                    
                }

                return new Response
                {
                    IsSuccess=true,
                    Message = "Usuario insertado Ok",
                    Result = user
                };

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

        public void SaveProducts(List<Product> products)
        {
            using (var da = new DataAccess())
            {
                //Deleting old data
                var oldProducts = da.GetList<Product>(false);
                foreach (var product in oldProducts)
                {
                    da.Delete(product);
                }

                //Adding new data
                foreach (var product in products)
                {
                    da.Insert(product);
                }
            }
        }

        public Response Login(string email, string password)
        {
            try
            {

                using (var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if(user== null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No hay conexión."
                        };
                    }

                    if(user.UserName.ToUpper() == email.ToUpper() && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Login ok", 
                            Result= user
                        };
                    }

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o clave incorrecto"
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

        internal List<Product> GetProducts()
        {
            using (var dat = new DataAccess())
            {
                return dat.GetList<Product>(true).OrderBy(x=>x.Description).ToList();
            }
        }
    }
}
