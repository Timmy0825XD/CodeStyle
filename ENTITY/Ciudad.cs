using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Ciudad
    {
        public string IdCiudad { get; set; }
        public string NombreCiudad { get; set; }

        public IList<Barrio> Barrios { get; set; }

        public Ciudad()
        {
            Barrios = new List<Barrio>();
        }
    }
}
