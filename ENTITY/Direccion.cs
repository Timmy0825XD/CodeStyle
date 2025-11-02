namespace ENTITY
{
    public class Direccion
    {
        public string IdDireccion { get; set; }
        public string DireccionTexto { get; set; }
        public string IdBarrio { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }

        public Barrio Barrio { get; set; }
    }
}