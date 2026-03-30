using Microsoft.EntityFrameworkCore.Migrations;
using salespoints.Infrastructure.Constants;
using salespoints.Infrastructure.Helpers;

#nullable disable

namespace salespoints.Infrastructure.Migrations
{
    public partial class sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateOrReplaceProcedures(
                ("sp_GetCriticalProducts.sql", StoredProcedures.Inventary.sp_GetCriticalProducts)
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP PROCEDURE IF EXISTS {StoredProcedures.Inventary.sp_GetCriticalProducts}");
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_OtroEjemplo");
        }
    }
}

