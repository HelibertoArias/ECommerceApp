using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using ECommerceApp.Data;
using System.Linq;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Media;
using ECommerceApp.Infrastructure;
using Plugin.Media.Abstractions;

namespace ECommerceApp.ViewModels
{
    public class CustomerItemViewModel : Customer, INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService navigationService;
        private NetService netService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private GeolocatorService geoLocatorService;


        private ImageSource imageSource;
        private bool isRunning;
        private MediaFile file;



        #endregion


        #region Properties

        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }
        public ObservableCollection<CityItemViewModel> Cities { get; set; }


        public ImageSource ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }

            }
            get { return imageSource; }
        }

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


        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands
        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }

        public ICommand NewCustomerCommand { get { return new RelayCommand(NewCustomer); } }

        //public ICommand RefreshCommandCommand { get { return new RelayCommand(RefreshCommand); } }


        private async void TakePicture()
        {
            IsRunning = true;
            //await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.ShowMessage("No Camera", ":( No camera available.");
            }
            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Photos",
                Name = "NewCustomer.jpg"
            });

            if (file == null)
                return;

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                   // file.Dispose(); Sin esto se puede acc
                    return stream;
                });
            }

            IsRunning = false;

        }
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


        private async void NewCustomer()
        {


            if (string.IsNullOrEmpty(UserName))
            {
                await dialogService.ShowMessage("error", "Debe ingresar email");
                return;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage("error", "Debe ingresar nombres");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage("error", "Debe ingresar apellidos");
                return;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.ShowMessage("error", "Debe ingresar telefono");
                return;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await dialogService.ShowMessage("error", "Debe ingresar dirección");
                return;
            }

            if (DepartmentId == 0)
            {
                await dialogService.ShowMessage("error", "Debe seleccionar un departamento");
                return;
            }

            if (CityId == 0)
            {
                await dialogService.ShowMessage("error", "Debe seleccionar una ciudad");
                return;
            }


            IsRunning = true;
            await geoLocatorService.GetLocatocation();


            var files = file.GetStream();
            var customer = new Customer()
            {
                Address = Address,
                CityId = CityId,
                DepartmentId = DepartmentId,
                FirstName = FirstName,
                IsUpdated = true,
                LastName = LastName,
                Latitude = geoLocatorService.Latitude,
                Longitude = geoLocatorService.Longitude,
                Phone = Phone,
                UserName = UserName,

            };


            var response = await apiService.NewCustomer(customer);
            if (response.IsSuccess && file != null)
            {
                customer = (Customer)response.Result;
                var response2 = await apiService.SetPhoto(customer.CustomerId, file.GetStream());
                var fileName = $"{customer.CustomerId}.jpg";
                var folder = "~/Content/Customers";
                var fullPath = System.IO.Path.Combine(folder, fileName);
                customer.Photo = fullPath;

                var response3 = await apiService.UpdateCustomer(customer);
            } 



            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await dialogService.ShowMessage("Confirmación", response.Message);

            await navigationService.Back();



        }





        #region Contructor
        public CustomerItemViewModel()
        {
            //--> Services
            navigationService = new NavigationService();
            netService = new NetService();
            dataService = new DataService();
            apiService = new ApiService();
            geoLocatorService = new GeolocatorService();

            dialogService = new DialogService();
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