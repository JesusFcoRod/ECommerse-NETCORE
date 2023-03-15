using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public int idMunicipio { get; set; }
        public string nombre { get; set; }

        //LLAVE FORANEA CON ESTADO
        public ML.Estado Estado { get; set; }

        //LISTA PARA OBTENER LOS MUNICIPIOS 
        public List<Object> Municipios { get; set; }
    }
}
