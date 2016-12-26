using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceApp.Models
{
    public class Order
    {
        [PrimaryKey]
        public int OrderId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public int StateId { get; set; }

        public string Date { get; set; }

        public string Remarks { get; set; }

        [ManyToOne]
        public Customer Customer { get; set; }

        public override int GetHashCode()
        {
            return OrderId;
        }
    }
}