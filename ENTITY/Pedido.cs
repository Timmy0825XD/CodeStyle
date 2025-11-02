namespace ENTITY
{
    public class Pedido
    {
        public string IdPedido { get; set; }
        public string NumeroPedido { get; set; }
        public string CedulaCliente { get; set; }
        public string IdDireccionEnvio { get; set; }
        public string IdMetodoPago { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string NotasCliente { get; set; }

        public Persona Cliente { get; set; }
        public Direccion DireccionEnvio { get; set; }
        public MetodoPago MetodoPago { get; set; }

        public IList<DetallePedido> Detalles { get; set; }

        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }
    }
}