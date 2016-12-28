using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using ECommerceApp.Data;
using System.Linq;

namespace ECommerceApp.ViewModels
{
    public class CustomerItemViewModel : Customer
    {
        #region Attributes
        private NavigationService navigationService;
        private NetService netService;
        private ApiService apiService;
        private DataService dataService;

        #endregion


        #region Properties

        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }
        public ObservableCollection<CityItemViewModel> Cities { get; set; } 
        #endregion

        #region Commands
        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

        #region Methods
        private async void CustomerDetail()
        {
            var customerItemViewModel = new CustomerItemViewModel
            {
                Address = Address,
                City = City,
                CityId = CityId,
                CompanyCustomers = CompanyCustomers,
                CustomerId = CustomerId,
                Department = Department,
                DepartmentId = DepartmentId,
                FirstName = FirstName,
                IsUpdated = IsUpdated,
                LastName = LastName,
                Latitude = Latitude,
                Longitude = Longitude,
                Orders = Orders,
                Phone = Phone,
                Photo = Photo,
                Sales = Sales,
                UserName = UserName
            };

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await navigationService.Navigate("CustomerDetailPage");
        }
        #endregion

        #region Contructor
        public CustomerItemViewModel()
        {
            //--> Services
            navigationService = new NavigationService();
            netService = new NetService();
            dataService = new DataService();
            apiService = new ApiService();

            //--> Listas
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            Cities = new ObservableCollection<CityItemViewModel>();

            //->Loading bindable pickers
            LoadDepartment();
            LoadCity();
        }
        #endregion

        #region Deparment
        private async void LoadDepartment()
        {
            var departaments = new List<Department>();

            if (netService.IsConnected())
            {
                departaments = await apiService.Get<Department>("Departments");
                dataService.Save(departaments);
            }
            else
            {
                departaments = dataService.Get<Department>(true);
            }

            ReloadDepartment(departaments);
        }

        private void ReloadDepartment(List<Department> departaments)
        {
            Departments.Clear();

            departaments = departaments.OrderBy(x => x.Name).ToList();

            foreach (var department in departaments)
            {
                Departments.Add(new DepartmentItemViewModel()
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartmentId = department.DepartmentId,
                    Name = department.Name
                });
            }
        }
        #endregion

        #region City
        private async void LoadCity()
        {
            var list = new List<City>();

            if (netService.IsConnected())
            {
                list = await apiService.Get<City>("Cities");
                dataService.Save(list);
            }
            else
            {
                list = dataService.Get<City>(true);
            }

            ReloadCity(list);
        }

        private void ReloadCity(List<City> list)
        {
            Cities.Clear();

            list = list.OrderBy(x => x.Name).ToList();

            foreach (var item in list)
            {
                Cities.Add(new CityItemViewModel()
                {
                    CityId = item.CityId,
                    Customers = item.Customers,
                    Department = item.Department,
                    DepartmentId = item.DepartmentId,
                    Name = item.Name
                });
            }
        }
        #endregion 
        #endregion
    }
}