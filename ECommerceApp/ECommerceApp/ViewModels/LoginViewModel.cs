using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }

    }
}
