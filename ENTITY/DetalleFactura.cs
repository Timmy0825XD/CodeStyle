namespace ENTITY
{
    public class DetalleFactura
    {
        public string IdDetalle { get; set; }
        public string IdFactura { get; set; }
        public string IdArticulo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal DescuentoLinea { get; set; }
        public decimal SubtotalLinea { get; set; }

        public Factura Factura { get; set; }
        public Articulo Articulo { get; set; }
    }
}