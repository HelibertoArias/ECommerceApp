namespace ECommerceApp.Interfaces
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }

        void CheckNetworkConnection();
    }
}