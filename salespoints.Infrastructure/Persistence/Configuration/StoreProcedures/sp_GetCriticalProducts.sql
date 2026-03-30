CREATE OR ALTER PROCEDURE sales.sp_GetCriticalProducts
    @SalesPointId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id AS product_id,
        p.code AS code_sku,
        p.name AS name,
        p.stock AS stock,
        p.minimum_stock AS minimum_stock
    FROM  [sales].[product_salespoints] inv
	inner join [catalog].[products] p on inv.product_id = p.id
	inner join [sales].[salespoints] s on inv.salespoint_id = s.id
	where s.id = @SalesPointId and p.minimum_stock > p.stock;
END