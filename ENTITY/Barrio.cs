namespace ENTITY
{
    public class Barrio
    {
        public string IdBarrio { get; set; }
        public string IdCiudad { get; set; }
        public string NombreBarrio { get; set; }

        public Ciudad Ciudad { get; set; }
        public IList<Direccion> Direcciones { get; set; }

        public Barrio()
        {
            Direcciones = new List<Direccion>();
        }
    }
}