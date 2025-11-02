namespace ENTITY
{
    public class DetallePedido
    {
        public string IdDetalle { get; set; }
        public string IdPedido { get; set; }
        public string IdVariante { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubtotalLinea { get; set; }

        public Pedido Pedido { get; set; }
        public VarianteArticulo Variante { get; set; }
    }
}