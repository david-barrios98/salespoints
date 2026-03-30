using salespoints.Application.DTOs.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salespoints.Application.Ports.Inbound
{
    public interface IGetCriticalInventoryUseCase
    {
        Task<IEnumerable<ProductCriticalDTO>> HandleAsync(int salesPointId);
    }
}