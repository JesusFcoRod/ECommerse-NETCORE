using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Colonia.FromSqlRaw($"[ColoniaGetByIdMunicipio] {idMunicipio}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objeto in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.idColonia = objeto.IdColonia;
                            colonia.nombre = objeto.Nombre;

                            result.Objects.Add(colonia);

                        }
                    }
                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}
