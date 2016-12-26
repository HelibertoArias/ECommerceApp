using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceApp.Models
{
    public class Inventory
    {
        [PrimaryKey]
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        [ManyToOne]
        public Product Product { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public double Stock { get; set; }

        public override int GetHashCode()
        {
            return InventoryId;
        }
    }
}