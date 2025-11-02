namespace ENTITY
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string IdRol { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public Rol Rol { get; set; }
        public Persona Persona { get; set; }
    }
}