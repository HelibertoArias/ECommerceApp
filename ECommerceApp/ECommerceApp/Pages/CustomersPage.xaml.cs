using ECommerceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ECommerceApp.Pages
{
    public partial class CustomersPage : ContentPage
    {
        public CustomersPage()
        {
            InitializeComponent();

            //Refresh list after insert a new customer
            var main = (MainViewModel)BindingContext;
            Appearing += (object sender, EventArgs e) =>
            {
                main.RefreshCustomersCommand.Execute(this);
            };
        }

      
    }
}
