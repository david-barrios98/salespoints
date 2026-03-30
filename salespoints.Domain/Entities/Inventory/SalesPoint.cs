using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace salespoints.Core.Domain.Entities
{
    [Table("salespoints", Schema = "sales")]
    public class SalesPoint
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

        [MaxLength(500)]
        [Column("address")]
        public string? adress { get; set; }

        [Column("active")]
        public bool active { get; set; } = true;

        [Column("create")]
        public DateTime create { get; set; } = DateTime.UtcNow;

        [Column("update")]
        public DateTime update { get; set; } = DateTime.UtcNow;

        // Relación many-to-many con Product
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}