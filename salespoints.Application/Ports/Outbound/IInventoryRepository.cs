using salespoints.Application.DTOs.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salespoints.Application.Ports.Outbound
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<ProductCriticalDTO>> GetCriticalProductsAsync(int salesPointId);
    }
}