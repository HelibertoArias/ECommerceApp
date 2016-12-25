using ECommerceApp.Data;
using ECommerceApp.Models;
using System.Collections.ObjectModel;

namespace ECommerceApp.ViewModels
{
    public class MainViewModel
    {
        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            return instance ?? (instance = new MainViewModel());
        }
        #endregion


        #region Attribute

        private DataService dataService;

        #endregion Attribute

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        #endregion Properties

        #region Contructors

        public MainViewModel()
        {
            //Singleton
            instance = this;

            //Create observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();

            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();

            //Create instences service
            dataService = new DataService();

            LoadMenu();
        }

        public void LoadUser(User user)
        {
            UserLoged.FullName = user.FullName;
            UserLoged.Photo = user.PhotoFullPath;
        }

        #endregion Contructors

        #region Methods

        private void LoadMenu()
        {
            AddItem("ic_action_product.png", "ProductsPage", "Productos");
            AddItem("ic_action_customer.png", "CustomersPage", "Clientes");
            AddItem("ic_action_order.png", "OrdersPage", "Pedidos");
            AddItem("ic_action_delivery.png", "DeliveriesPage", "Entregas");
            AddItem("ic_action_sync.png", "SyncPage", "Sincronizar");
            AddItem("ic_action_setup.png", "SetupPage", "Configuración");
            AddItem("ic_action_logout.png", "LogoutPage", "Salir");
        }

        private void AddItem(string icon, string pageName, string title)
        {
            Menu.Add(new MenuItemViewModel { Icon = icon, PageName = pageName, Title = title });
        }

        #endregion Methods
    }
}