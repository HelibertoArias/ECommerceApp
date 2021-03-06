﻿using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Data
{
    public class DataService
    {
        #region Users
        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }
        }

        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
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

        public Response InsertUser(User user)
        {
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
                    IsSuccess = true,
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

        public Response Login(string email, string password)
        {
            try
            {

                using (var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if (user == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No hay conexión."
                        };
                    }

                    if (user.UserName.ToUpper() == email.ToUpper() && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Login ok",
                            Result = user
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

        #endregion

        #region Products

        public List<Product> GetProducts(string filter)
        {
            using (var dat = new DataAccess())
            {
                return dat.GetList<Product>(true)
                    .Where(p => p.Description.ToUpper().Contains(filter.ToUpper())  || 
                            p.BarCode.Contains(filter))
                    .OrderBy(x => x.Description)
                    .ToList();
            }
        }


        #endregion

        #region Customers
        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var dat = new DataAccess())
            {
                return dat.GetList<T>(withChildren).ToList();
            }
        }

        public List<Customer> GetCustomers(string filter)
        {
            using (var dat = new DataAccess())
            {
                return dat.GetList<Customer>(true)
                    .Where(p => p.FirstName.ToUpper().Contains(filter.ToUpper())
                                || p.LastName.ToUpper().Contains(filter.ToUpper())
                          )
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ToList();
            }
        }

        public void Save<T>(List<T> list) where T : class
        {
            using (var da = new DataAccess())
            {
                //Deleting old data
                var oldRecords = da.GetList<T>(false);
                foreach (var record in oldRecords)
                {
                    da.Delete(record);
                }

                //Adding new data
                foreach (var record in list)
                {
                    da.Insert(record);
                }
            }
        }

        public T Find<T>(int pk, bool withChildren) where T :class
        {
            using (var da= new DataAccess())
            {
                return da.Find<T>(pk, withChildren);
            }
        }

        public void Update<T>(T model) 
        {
            using (var da = new DataAccess())
            {
                da.Update(model);
            }
        }


        #endregion




    }
}
