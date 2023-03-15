using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public int idRol { get; set; }
        public string descripcion { get; set; }

        //PROPIEDAD PARA LISTAR LOS ROLES
        public List<object> Roles { get; set; }
    }
}
