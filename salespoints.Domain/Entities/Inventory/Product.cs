using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace salespoints.Core.Domain.Entities
{
    [Table("products", Schema = "catalog")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("code")]
        public string code { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("name")]
        public string name { get; set; }

        [MaxLength(1000)]
        [Column("description")]
        public string? description { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public decimal price { get; set; }

        [Required]
        [Column("stock")]
        public int stock { get; set; }

        [Required]
        [Column("minimum_stock")]
        public int Minimum_stock { get; set; }

        [Column("active")]
        public bool active { get; set; } = true;

        [Column("create")]
        public DateTime create { get; set; } = DateTime.UtcNow;

        [Column("update")]
        public DateTime update { get; set; } = DateTime.UtcNow;

        // Relación many-to-many con SalesPoint
        public ICollection<SalesPoint> SalesPoints { get; set; } = new List<SalesPoint>();
    }
}