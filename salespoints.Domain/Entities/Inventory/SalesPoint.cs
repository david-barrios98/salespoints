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
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("code")]
        public string Code { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column("address")]
        public string? Address { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        [Column("create")]
        public DateTime Create { get; set; } = DateTime.UtcNow;

        [Column("update")]
        public DateTime Update { get; set; } = DateTime.UtcNow;

        // Relación many-to-many con Product
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}