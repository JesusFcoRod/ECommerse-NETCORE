using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int idDireccion { get; set; }
        public string calle { get; set; }
        public string numeroInterio { get; set; }

        public string numeroExterior { get; set; }

        //LLAVES FORANEAS
        public ML.Colonia Colonia { get; set; }

        public ML.Usuario Usuario { get; set; }
    }
}
