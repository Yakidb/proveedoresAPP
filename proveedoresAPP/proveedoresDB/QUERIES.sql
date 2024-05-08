SELECT
    p.*,
    r.FechaCreacion
FROM PEDIDOS p
JOIN COTIZACIONES c ON p.idCotizacion = c.idCotizacion
JOIN REQUESICIONES r ON c.idRequisicion = r.idRequisicion
WHERE MONTH(r.FechaCreacion) = MONTH(GETDATE());

SELECT
  pr.*
FROM PEDIDOS p
JOIN COTIZACIONES c ON p.idCotizacion = c.idCotizacion
JOIN PROVEEDORES pr ON c.idProveedor = pr.idProveedor
WHERE p.Estatus = 1
ORDER BY pr.RazonSocial


SELECT
  pr.*,
  r.NumRequisicion AS 'Número Requisición',
  c.NumCotizacion AS 'Número Cotización'
FROM REQUESICIONES r
JOIN COTIZACIONES c ON r.idRequisicion = c.idRequisicion
JOIN PROVEEDORES pr ON c.idProveedor = pr.idProveedor
WHERE r.Estatus = '1'
AND c.Cancelada = 0
AND NOT EXISTS (
  SELECT 1
  FROM PEDIDOS p
  WHERE p.idCotizacion = c.idCotizacion)
  ORDER BY pr.RazonSocial;

