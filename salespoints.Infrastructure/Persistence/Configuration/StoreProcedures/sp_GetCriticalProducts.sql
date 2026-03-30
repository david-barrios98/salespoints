CREATE OR ALTER PROCEDURE sales.sp_GetCriticalProducts
    @SalesPointId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id AS product_id,
        p.code AS code_sku,
        p.name AS name,
        inv.stock AS stock,
        inv.minimum_stock AS minimum_stock
    FROM inventory.inventory inv
    INNER JOIN catalog.products p ON p.id = inv.product_id
    WHERE inv.salespoint_id = @SalesPointId
      AND inv.stock < inv.minimum_stock;
END