using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceApp.Models
{
    public class CompanyCustomer
    {
        [PrimaryKey]
        public int CompanyCustomerId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        [ManyToOne]
        public Company Company { get; set; }

        [ManyToOne]
        public Customer Customer { get; set; }

        public override int GetHashCode()
        {
            return CompanyCustomerId;
        }
    }
}