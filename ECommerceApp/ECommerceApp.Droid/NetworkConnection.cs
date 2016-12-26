

using Android.Content;
using Android.Net;
using ECommerceApp.Droid;
using ECommerceApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ECommerceApp.Droid.NetworkConnection))]

namespace ECommerceApp.Droid
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}