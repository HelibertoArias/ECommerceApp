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



        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }

        #region Commands
        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

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

        public CustomerItemViewModel() {
            //
            navigationService = new NavigationService();
            netService = new NetService();
            dataService = new DataService();
            apiService = new ApiService();

            Departments = new ObservableCollection<DepartmentItemViewModel>();

            LoadDepartment();
        }

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
                    DepartmentId=department.DepartmentId,
                    Name= department.Name
                });  
            }   
        }
    }
}