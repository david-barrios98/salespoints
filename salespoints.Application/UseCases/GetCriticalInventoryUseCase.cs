using System.Collections.Generic;
using System.Threading.Tasks;
using salespoints.Application.DTOs.Inventory;
using salespoints.Application.Ports.Inbound;
using salespoints.Application.Ports.Outbound;

namespace salespoints.Application.UseCases
{
    public class GetCriticalInventoryUseCase : IGetCriticalInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetCriticalInventoryUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<ProductCriticalDTO>> HandleAsync(int salesPointId)
        {
            // Lógica de dominio adicional (si hace falta) puede colocarse aquí.
            return await _inventoryRepository.GetCriticalProductsAsync(salesPointId);
        }
    }
}