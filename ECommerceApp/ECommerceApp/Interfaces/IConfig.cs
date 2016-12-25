using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    public interface IConfig
    {
        string DirectorioDB { get; }
        ISQLitePlatform Plataform { get; }
    }
}
