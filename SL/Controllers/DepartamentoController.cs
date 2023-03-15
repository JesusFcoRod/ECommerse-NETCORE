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
        [Route("api/Departamento/Update")]
        public ActionResult Update([FromBody] ML.Departamento departamento)
        {

            ML.Result result = BL.Departamento.DepartamentoUpdate(departamento);

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
        [Route("api/Departamento/Delete")]

        public ActionResult Delete([FromBody] ML.Departamento departamento)
        {

            ML.Result result = BL.Departamento.DepartamentoDelete(departamento);

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
        [Route("api/Departamento/GetById")]

        public ActionResult GetById([FromBody] ML.Departamento departamento)
        {

            ML.Result result = BL.Departamento.DepartamentoGetById(departamento);

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
