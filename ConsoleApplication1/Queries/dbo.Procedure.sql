CREATE PROCEDURE [dbo].[CountTotalCostByClient]
    @ClientId int
AS
SELECT Sum(p.ProductPrice*od.ProductQuantity) From Orders o, OrderDetails od, Clients c, Products p 
WHERE o.ClientID=c.ID AND od.OrderID=o.ID AND od.ProductID=p.ProductId AND c.ID = @ClientId