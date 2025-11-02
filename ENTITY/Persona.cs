using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Persona
    {
        public string Cedula { get; set; }
        public string IdDireccion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoSecundario { get; set; }
        public string CorreoPrincipal { get; set; }
        public string CorreoSecundario { get; set; }

        public Direccion Direccion { get; set; }
        public IList<Pedido> Pedidos { get; set; }
        public IList<Factura> Facturas { get; set; }

        public Persona()
        {
            Pedidos = new List<Pedido>();
            Facturas = new List<Factura>();
        }
    }
}
