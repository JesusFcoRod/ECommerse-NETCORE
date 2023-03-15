using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();

            ML.Result result = BL.Departamento.DepartamentoGetAll();
            departamento.Departamentos = result.Objects;

            return View(departamento);
        }

        [HttpGet]
        public ActionResult Form(int? idDepartamento)
        {
            ML.Result resultAreas = BL.Area.AreaGetlAll();
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            if (resultAreas.Correct)
            {
                departamento.Area.Areas = resultAreas.Objects;
            }

            if (idDepartamento != null)
            {
                departamento.idDepartamento = idDepartamento.Value;
                ML.Result result = BL.Departamento.DepartamentoGetById(departamento);

                if (result.Correct)
                {
                    departamento = (ML.Departamento)result.Object;//unboxing
                    departamento.Area.Areas = resultAreas.Objects;

                    return View(departamento);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del usuario" + resultAreas.ErrorMessage;
                    return View("Modal");
                }

            }
            else
            {
                return View(departamento);
            }

        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            if (departamento.idDepartamento > 0)
            {
                //UPDATE
                result = BL.Departamento.DepartamentoUpdate(departamento);
                ViewBag.Message = "Se ha actualizado el registro";
            }
            else
            {
                //ADD
                result = BL.Departamento.DepartamentoAdd(departamento);
                ViewBag.Message = "Se ha agregadao el nuevo el registro";

            }

            if (result.Correct == true)
            {

                return PartialView("Modal");
            }
            else
            {
                return PartialView("Modal");
            }
        }


    }
}
