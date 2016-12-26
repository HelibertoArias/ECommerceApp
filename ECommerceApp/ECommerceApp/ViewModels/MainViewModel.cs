using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ECommerceApp.ViewModels
{
    public class MainViewModel
    {
        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                instance = new MainViewModel();
            return instance;
        }

        #endregion Singleton

        #region Attribute

        private DataService dataService;
        private ApiService apiService;
        private NetService netService;

        #endregion Attribute

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

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
            Products = new ObservableCollection<ProductItemViewModel>();

            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();

            //Create instences service
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetService();

            LoadMenu();
            LoadProducts();
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

        private async void LoadProducts()
        {

            var products = new List<Product>();

            if (netService.IsConnected())
            {
                products = await apiService.GetProducts();
                dataService.SaveProducts(products);
            }
            else
            {
                products = dataService.GetProducts();
            }

            Products.Clear();

            foreach (var product in products)
            {
                Products.Add(new ProductItemViewModel()
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Company = product.Company,
                    CompanyId = product.CompanyId,
                    Description = product.Description,
                    Image = product.Image,
                    Inventories = product.Inventories,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Remarks = product.Remarks,
                    Stock = product.Stock,
                    Tax = product.Tax,
                    TaxId = product.TaxId
                });
            }
        }

        private void AddItem(string icon, string pageName, string title)
        {
            Menu.Add(new MenuItemViewModel { Icon = icon, PageName = pageName, Title = title });
        }

        #endregion Methods
    }
}