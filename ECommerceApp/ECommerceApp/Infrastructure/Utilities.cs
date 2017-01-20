using System.Text.RegularExpressions;

namespace ECommerceApp.Infrastructure
{
    public class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}").Success;
        }
    }
}