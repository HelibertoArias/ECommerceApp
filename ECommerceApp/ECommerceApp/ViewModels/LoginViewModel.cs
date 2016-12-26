using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attribute

        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        private DataService dataService;
        private NetService netService;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isRunning;

        #endregion Attribute

        #region Properties

        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get { return isRunning; }
        }

        #endregion Properties

        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            IsRemembered = true;
            apiService = new ApiService();
            dataService = new DataService();
            IsRemembered = true;
            netService = new NetService();
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
            var response = new Response();

            if (netService.IsConnected())
            {
                response = await apiService.Login(User, Password);               
            }
            else
            {
                response =  dataService.Login(User, Password);
            }


            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }



            var user = (User)response.Result;
            user.IsRemembered = IsRemembered;
            user.Password = Password;

            dataService.InsertUser(user);

            navigationService.SetMainPage(user);
        }
    }
}