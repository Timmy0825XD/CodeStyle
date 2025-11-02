using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Factura
    {
        public string IdFactura { get; set; }
        public string NumFactura { get; set; }
        public string IdPedido { get; set; }
        public string Cufe { get; set; }
        public string IdCliente { get; set; }
        public string IdUsuario { get; set; }
        public string IdMetodoPago { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CodigoQr { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal SubtotalFactura { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string EstadoFactura { get; set; }
        public string EstadoDian { get; set; }

        public Pedido Pedido { get; set; }
        public Persona Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public MetodoPago MetodoPago { get; set; }

        public IList<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }
    }
}
