using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace ECommerceApp.ViewModels
{
    public class MenuItemViewModel
    {
        #region Attributes
        private NavigationService navigationService; 
        #endregion

        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }
        //public MenuItemViewModel(string icon, string pageName, string title)
        //{
        //    this.Icon = icon;
        //    this.PageName = pageName;
        //    this.Title = title;

        //    navigationService = new NavigationService();
        //}
        #region Properties

        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private async void Navigate()
        {
            await navigationService.Navigate(PageName);
        }
    }
}
