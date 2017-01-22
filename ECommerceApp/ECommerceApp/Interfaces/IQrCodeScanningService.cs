using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }
}