using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class DepartamentoController : Controller
    {

        [HttpGet]
        [Route("api/Departamento/GetAll")]
        public ActionResult GetAll()
        {

            ML.Result result = BL.Departamento.DepartamentoGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Departamento/Add")]
        public ActionResult Add([FromBody] ML.Departamento departamento)
        {

            ML.Result result = BL.Departamento.DepartamentoAdd(departamento);

            if (result.Correct)
            {
                return Ok(result); 
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Departamento/Update/{idDepartamento}")]
        public ActionResult Update(int IdDepartamento, [FromBody] ML.Departamento departamento)
        {

            ML.Result result = BL.Departamento.DepartamentoUpdate(departamento,IdDepartamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/Departamento/Delete/{idDepartamento}")]

        public ActionResult Delete(int idDepartamento)
        {

            ML.Result result = BL.Departamento.DepartamentoDelete(idDepartamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }


        [HttpGet]
        [Route("api/Departamento/GetById/{idDepartamento}")]

        public ActionResult GetById(int idDepartamento)
        {

            ML.Result result = BL.Departamento.DepartamentoGetById(idDepartamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

    }

}
