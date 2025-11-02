namespace ENTITY
{
    public class MetodoPago
    {
        public string IdMetodo { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }

        public IList<Pedido> Pedidos { get; set; }
        public IList<Factura> Facturas { get; set; }

        public MetodoPago()
        {
            Pedidos = new List<Pedido>();
            Facturas = new List<Factura>();
        }
    }
}