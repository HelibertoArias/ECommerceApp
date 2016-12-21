using ECommerceApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ECommerceApp
{
    public partial class App : Application
    {
        #region Attributes

        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; } 
        #endregion

        public App()
        {
            InitializeComponent();

            MainPage = new ECommerceApp.Pages.LoginPage();
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
