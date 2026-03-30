namespace salespoints.Application.DTOs.Inventory
{
    public class ProductCriticalDTO
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
    }
}