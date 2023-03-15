using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Departamento
    {
        public int idDepartamento { get; set; }
        public string nombre { get; set; }

        //PROPIEDAD DE NAVEGACION 
        public ML.Area Area { get; set; }

        //LISTAS
        public List<object> Departamentos { get; set; }



    }
}
