using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace ECommerceApp.Models
{
    public class Tax
    {
        [PrimaryKey]
        public int TaxId { get; set; }

        public string Description { get; set; }

        public double Rate { get; set; }

        public int CompanyId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

        public override int GetHashCode()
        {
            return TaxId;
        }
    }
}