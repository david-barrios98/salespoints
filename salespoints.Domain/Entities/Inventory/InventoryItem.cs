using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace salespoints.Core.Domain.Entities.Inventory
{
    [Table("product_salespoints", Schema = "sales")]
    public class InventoryItem
    {

        [Column("product_id")]
        public int product_id { get; set; }
        [Column("salespoint_id")]
        public int salespoint_id { get; set; }
        // Navegación
        public salespoints.Core.Domain.Entities.Product? Product { get; set; }
        public salespoints.Core.Domain.Entities.SalesPoint? SalesPoint { get; set; }
    }
}