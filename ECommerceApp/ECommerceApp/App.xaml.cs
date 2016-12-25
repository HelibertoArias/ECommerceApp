using ECommerceApp.Data;
using ECommerceApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ECommerceApp.Models;

namespace ECommerceApp
{
    public partial class App : Application
    {
        #region Attributes
        private DataService dataService;

  #endregion

        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }

        public App()
        {
            InitializeComponent( );
            dataService = new DataService();


            var user = dataService.GetUser();

            if(user!=null && user.IsRemembered){
                App.CurrentUser = user;
                MainPage =new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }

            //MainPage = new ECommerceApp.Pages.LoginPage();
        }
        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
