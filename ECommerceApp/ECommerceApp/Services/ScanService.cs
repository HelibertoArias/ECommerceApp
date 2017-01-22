using ECommerceApp.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ECommerceApp.Services
{
    public class ScanService
    {
        public async Task<string> Scanner()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                var result = await scanner.ScanAsync();
                return result.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }
    }
}