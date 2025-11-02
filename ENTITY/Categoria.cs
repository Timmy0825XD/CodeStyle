namespace ENTITY
{
    public class Categoria
    {
        public string IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public IList<Articulo> Articulos { get; set; }
        public Categoria()
        {
            Articulos = new List<Articulo>();
        }
    }
}
