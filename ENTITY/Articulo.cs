using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Articulo
    {
        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Genero { get; set; }
        public string Material { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public string ImagenPrincipal { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public IList<Categoria> Categorias { get; set; }
        public IList<VarianteArticulo> Variantes { get; set; }
        public IList<ImagenArticulo> Imagenes { get; set; }

        public Articulo()
        {
            Categorias = new List<Categoria>();
            Variantes = new List<VarianteArticulo>();
            Imagenes = new List<ImagenArticulo>();
        }
    }
}
