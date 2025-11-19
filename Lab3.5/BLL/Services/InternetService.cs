using DAL.Interfaces;
using System.Net.NetworkInformation;

namespace PL.Services
{
    // Реальна реалізація для консолі
    public class ConsoleInternetService : IInternetService
    {
        public bool IsConnected
        {
            get
            {
                try
                {
                    return NetworkInterface.GetIsNetworkAvailable();
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}