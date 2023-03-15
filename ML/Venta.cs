using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Venta
    {
        public int idVenta { get; set; }
        public decimal total { get; set; }
        public string fecha { get; set; }
        public ML.MetodoPago MetodoPago { get; set; }



    }
}
