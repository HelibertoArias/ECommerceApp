using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel
    {
        #region Attribute
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        #endregion

        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }

        public bool IsRunning { get; set; }
        #endregion

        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            IsRemembered = true;
            apiService = new ApiService();
        }

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar usuario");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una contraseña");
                return;
            }
            IsRunning = true;
            var response = await apiService.Login(User, Password);
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }


            navigationService.SetMainPage();
        }

    }
}
