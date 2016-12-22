using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
           await App.Current.MainPage.DisplayAlert(title, message, "Acceptar");
        }
    }
}
