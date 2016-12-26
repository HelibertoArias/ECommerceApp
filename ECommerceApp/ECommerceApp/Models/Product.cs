using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace ECommerceApp.Models
{
    public class Product
    {
        [PrimaryKey]
        public int ProductId { get; set; }

        public string Description { get; set; }

        public string BarCode { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Remarks { get; set; }

        public double Stock { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Inventory> Inventories { get; set; }

        public int CompanyId { get; set; }

        [ManyToOne]
        public Company Company { get; set; }

        public int CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        public int TaxId { get; set; }

        [ManyToOne]
        public Tax Tax { get; set; }

        public string ImageFullPath { get { return string.Format("http://zulu-software.com/ECommerce{0}", Image.Substring(1)); } }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }
}