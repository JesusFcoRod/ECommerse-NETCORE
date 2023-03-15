﻿using Microsoft.AspNetCore.Mvc;
using ML;

namespace SL.Controllers
{
    public class UsuarioController : Controller
    {
        //Por lo general en los Post se utiliza el pase de parametros a traves del "Body"

        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.UsuarioGetAll(usuario);

            if (result.Correct)
            {
                return Ok(result); // 200
            }
            else
            {
                return NotFound(result); //Error 404
            }
        }

        [HttpPost]
        [Route("api/Usuario/Add")]       
        public ActionResult Add([FromBody] ML.Usuario usuario)
        {
          
            ML.Result result = BL.Usuario.UsuarioAdd(usuario);

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
        [Route("api/Usuario/Update")]
        public ActionResult Update([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.UsuarioUpdate(usuario);

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
        [Route("api/Usuario/Delete")]
        public ActionResult Delete([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.UsuarioDelete(usuario);

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
        [Route("api/Usuario/GetById")]
        public ActionResult GetById([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.UsuarioGetAllById(usuario);

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
