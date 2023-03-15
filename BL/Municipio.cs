using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Municipios.FromSqlRaw($"[MunicipioGetByIdEstado] {idEstado}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objeto in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.idMunicipio = objeto.IdMunicipio;
                            municipio.nombre = objeto.Nombre;

                            result.Objects.Add(municipio);

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
