using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class VarianteArticulo
    {
        public string IdVariante { get; set; }
        public string IdArticulo { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public string CodigoSku { get; set; }
        public int Stock { get; set; }
        public decimal PrecioAdicional { get; set; }
        public string Estado { get; set; }
        public Articulo Articulo { get; set; }
    }
}
