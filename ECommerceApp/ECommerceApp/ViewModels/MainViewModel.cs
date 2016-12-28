using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms.Maps;

namespace ECommerceApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        //-->Filters
        private string productsFilter;
        private string customersFilter;

        #endregion Attribute

        #region Properties

        //-> Observable list
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        public ObservableCollection<Pin> Pins{ get; set; }


        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        public CustomerItemViewModel CurrentCustomer { get; set; }


        //-> Filters
        public string ProductsFilter
        {
            set
            {
                if (productsFilter != value)
                {
                    productsFilter = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsFilter"));
                        if (string.IsNullOrEmpty(productsFilter))
                        {
                            LoadLocalProducts();
                        }
                    }
                }
            }
            get { return productsFilter; }
        }

        public string CustomersFilter
        {
            set
            {
                if (customersFilter != value)
                {
                    customersFilter = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomersFilter"));
                        if (string.IsNullOrEmpty(customersFilter))
                        {
                            LoadLocalCustomers();
                        }
                    }
                }
            }
            get { return customersFilter; }
        }

        #endregion Properties

        #region Contructors

        public MainViewModel()
        {
            //Singleton
            instance = this;

            //Create observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductItemViewModel>();
            Customers = new ObservableCollection<Customer>();
            Pins = new ObservableCollection<Pin>();

            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            CurrentCustomer = new CustomerItemViewModel();

            //Create instences service
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetService();

            LoadMenu();
            LoadProducts();
            LoadCustomers();
        }


        #endregion Contructors



        public void LoadUser(User user)
        {
            UserLoged.FullName = user.FullName;
            UserLoged.Photo = user.PhotoFullPath;
        }

        #region Methods

        private void AddItem(string icon, string pageName, string title)
        {
            Menu.Add(new MenuItemViewModel { Icon = icon, PageName = pageName, Title = title });
        }

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

        public void GetGeolocation()
        {
            var position1 = new Position(6.2652880, -75.5098530);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Pin1",
                Address = "prueba pin1"
            };
            Pins.Add(pin1);

            var position2 = new Position(6.2652880, -75.4598530);
            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Pin2",
                Address = "prueba pin2"
            };
            Pins.Add(pin2);

            var position3 = new Position(6.2652880, -75.4898530);
            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Pin3",
                Address = "prueba pin3"
            };
            Pins.Add(pin3);

        }


        #region Customers
        private void LoadLocalCustomers()
        {
            var customers = dataService.Get<Customer>(true);
            ReloadCustomers(customers);
        }

        private async void LoadCustomers()
        {
            var customers = new List<Customer>();

            if (netService.IsConnected())
            {
                customers = await apiService.Get<Customer>("Customers");
                dataService.Save<Customer>(customers);
            }
            else
            {
                customers = dataService.Get<Customer>(true);
            }

            Customers.Clear();

            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();
            customers = customers.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();

            foreach (var customer in customers)
            {
                Customers.Add(new CustomerItemViewModel
                {
                    Address = customer.Address,
                    City = customer.City,
                    CityId = customer.CityId,
                    CompanyCustomers = customer.CompanyCustomers,
                    CustomerId = customer.CustomerId,
                    Department = customer.Department,
                    DepartmentId = customer.DepartmentId,
                    FirstName = customer.FirstName,
                    IsUpdated = customer.IsUpdated,
                    LastName = customer.LastName,
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude,
                    Orders = customer.Orders,
                    Phone = customer.Phone,
                    Photo = customer.Photo,
                    Sales = customer.Sales,
                    UserName = customer.UserName
                });
            }
        }

        #endregion

        #region Products
        private void LoadLocalProducts()
        {
            var products = dataService.Get<Product>(true);
            ReloadProducts(products);
        }

        private async void LoadProducts()
        {

            var products = new List<Product>();

            if (netService.IsConnected())
            {
                products = await apiService.Get<Product>("Products");
                dataService.Save<Product>(products);
            }
            else
            {
                products = dataService.Get<Product>(true);
            }

            ReloadProducts(products);
        }

        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();
            products = products.OrderBy(x => x.Description).ToList();

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


        #endregion

        public void SetCurrentCustomer(CustomerItemViewModel customerItemViewModel)
        {
            CurrentCustomer = customerItemViewModel;
        }
        #endregion Methods

        #region Command
        public ICommand SearchProductCommand { get { return new RelayCommand(SearchProduct); } }
        public ICommand SearchCustomerCommand { get { return new RelayCommand(SearchCustomer); } }

        private void SearchProduct()
        {
            var products = dataService.GetProducts(ProductsFilter);
            ReloadProducts(products);

        }
        private void SearchCustomer()
        {
            var customers = dataService.GetCustomers(CustomersFilter);
            ReloadCustomers(customers);
        }




        #endregion
    }
}