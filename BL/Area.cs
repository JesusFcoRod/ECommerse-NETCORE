using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {

        public static ML.Result AreaGetlAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Areas.FromSqlRaw("[AreaGetAll]").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var objetoArea in query)
                        {
                            ML.Area area = new ML.Area();

                            area.idArea = objetoArea.IdArea;
                            area.nombre = objetoArea.Nombre;

                            result.Objects.Add(area);
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
