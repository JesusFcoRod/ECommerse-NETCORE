using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result PaisGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Pais.FromSqlRaw("PaisGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var objetoPais in query)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.idPais = objetoPais.IdPais;
                            pais.nombre = objetoPais.Nombre;

                            result.Objects.Add(pais);
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
