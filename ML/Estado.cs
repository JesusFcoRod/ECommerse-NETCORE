using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        public int idEstado { get; set; }
        public string nombre { get; set; }

        //LLAVE FORANEA CON PAIS
        public ML.Pais Pais { get; set; }

        //LISTA PARA OBTENER TODOS LOS ESTADOS
        public List<Object> Estados { get; set; }
    }
}
