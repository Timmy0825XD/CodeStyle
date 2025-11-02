using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ImagenArticulo
    {
        public string IdImagen { get; set; }
        public string IdArticulo { get; set; }
        public string UrlImagen { get; set; }
        public int Orden { get; set; }
        public string EsPrincipal { get; set; }
        public Articulo Articulo { get; set; }
    }
}
