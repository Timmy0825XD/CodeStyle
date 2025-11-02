using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Rol
    {
        public string IdRol { get; set; }
        public string NombreRol { get; set; }
        public string Estado { get; set; }
        public IList<Usuario> Usuarios { get; set; }
        public Rol()
        {
            Usuarios = new List<Usuario>();
        }
    }
}
