using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region Contructors
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            LoadMenu();
        }

        #endregion

        #region Methods

        private void LoadMenu()
        { 
            AddItem("ic_action_product.png", "ProductsPage", "Productos");
            AddItem("ic_action_customer.png", "CustomersPage", "Clientes");
            AddItem("ic_action_order.png", "OrdersPage", "Pedidos");
            AddItem("ic_action_delivery.png", "DeliveriesPage", "Entregas");
            AddItem("ic_action_sync.png", "SyncPage", "Sincronizar");
            AddItem("ic_action_setup.png", "SetupPage", "Configuración");
            AddItem("ic_action_logout.png", "Logout", "Salir");
        }

        private void AddItem(string icon, string pageName, string title)
        {
            Menu.Add(new MenuItemViewModel { Icon = icon, PageName = pageName, Title = title });
        }
        #endregion
    }
}
