using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezProgramacionNcapasContext context = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw("[ProveedorGetAll]").AsEnumerable().ToList();
                    result.Objects = new List<object>();

                    foreach (var objeto in query)
                    {
                        ML.Proveedor proveedor = new ML.Proveedor();
                        proveedor.IdProveedor = objeto.IdProveedor;
                        proveedor.Telefono = objeto.Telefono;

                        result.Objects.Add(proveedor);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }
    }
}
