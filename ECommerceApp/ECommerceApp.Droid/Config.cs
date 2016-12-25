using ECommerceApp.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(ECommerceApp.Droid.Config))]

namespace ECommerceApp.Droid
{
    public class Config : IConfig
    {
        private string directorioDB;
        private ISQLitePlatform plataform;

        public string DirectorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(directorioDB))
                {
                    directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return directorioDB;
            }
        }

        public ISQLitePlatform Plataform
        {
            get
            {
                if (plataform == null)
                    plataform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                return plataform;
            }
        }
    }
}