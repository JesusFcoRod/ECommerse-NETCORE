using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class Rol
    {
        public static ML.Result RolGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Rols.FromSqlRaw("RolGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var objetoRol in query)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.idRol = objetoRol.IdRol;
                            rol.descripcion = objetoRol.Descripcion;

                            result.Objects.Add(rol);
                        }
                    }
                }
                result.Correct = true;

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

