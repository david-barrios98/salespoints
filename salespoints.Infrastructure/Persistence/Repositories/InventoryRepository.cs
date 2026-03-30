using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using salespoints.Application.DTOs.Inventory;
using salespoints.Application.Ports.Outbound;
using salespoints.Infrastructure.Persistence.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace salespoints.Infrastructure.Persistence.Repositories
{
    public class InventoryRepository : SqlConfigServer, IInventoryRepository
    {
        public InventoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<ProductCriticalDTO>> GetCriticalProductsAsync(int salesPointId)
        {
            var sqlParams = new SqlParameter[]
            {
                CreateParameter("@SalesPointId", salesPointId, SqlDbType.Int)
            };

            var list = await ExecuteStoredProcedureAsync(
                "inventory.sp_GetCriticalProducts",
                sqlParams,
                reader =>
                {
                    return new ProductCriticalDTO
                    {
                        ProductId = reader.GetInt32(0),
                        Code = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Stock = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        MinimumStock = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                    };
                });

            return list;
        }
    }
}