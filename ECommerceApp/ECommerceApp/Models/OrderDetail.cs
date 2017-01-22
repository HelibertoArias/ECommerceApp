using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    public class OrderDetail
    {
        [PrimaryKey, AutoIncrement]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

        public double TaxRate { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public decimal Value { get { return Price * (decimal)Quantity; } }

        [ManyToOne]
        public Product Product { get; set; }

        [ManyToOne]
        public Order Order { get; set; }

        public override int GetHashCode()
        {
            return OrderDetailId;
        }


    }
}
