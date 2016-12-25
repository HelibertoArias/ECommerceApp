using ECommerceApp.Pages;
using System.Threading.Tasks;
using System;
using ECommerceApp.Models;
using ECommerceApp.Data;
using ECommerceApp.ViewModels;

namespace ECommerceApp.Services
{
    public class NavigationService
    {
        private DataService dataService;
       public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false; /*Oculta el menú lateral al seleccionar*/
            switch (pageName)
            {
                case "CustomersPage":
                    await App.Navigator.PushAsync(new CustomersPage());
                    break;

                case "DeliveriesPage":
                    await App.Navigator.PushAsync(new DeliveriesPage());
                    break;

                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;

                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;

                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;

                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;

                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;

                case "LogoutPage":
                    Logout();
                    break;

                default:
                    break;
            }
        }

        //internal User GetCurrentUser()
        //{
        //    throw new NotImplementedException();
        //}

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false;
            dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new LoginPage();

        }
        internal void SetMainPage(User user)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoadUser(user);
            App.CurrentUser = user;
            App.Current.MainPage = new MasterPage();
        }
    }
}