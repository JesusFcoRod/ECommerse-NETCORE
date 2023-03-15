using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        public int idColonia { get; set; }
        public string nombre { get; set; }
        public string codigoPostal { get; set; }

        //LLAVE FORANEA CON MUNICIPIO
        public ML.Municipio Municipio { get; set; }

        //LISTA PARA LAS COLONIAS
        public List<Object> Colonias { get; set; }
    }
}
