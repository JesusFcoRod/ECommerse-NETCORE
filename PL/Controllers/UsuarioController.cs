using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = BL.Usuario.UsuarioGetAll(usuario);

            //usuario.Usuarios = result.Objects;
            //return View(usuario);

            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    usuario.Usuarios = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(usuario);

        }

        //FORMULARIO PARA FILTRAR USUARIOS(POST)

        [HttpPost]

        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UsuarioGetAll(usuario);


            usuario.Usuarios = result.Objects;
            return View(usuario);

        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {

            ML.Result resultRol = BL.Rol.RolGetAll();//btener la lista de Roles
            ML.Result resultPais = BL.Pais.PaisGetAll();


            ML.Usuario usuario = new ML.Usuario(); //Objeto alumno para enviar a la vista 
            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


            if (resultRol.Correct && resultPais.Correct)
            {

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Rol.Roles = resultRol.Objects;// Asingnar la lista de objects a roles. 
            }

            if (idUsuario != null)
            {
                usuario.idUsuario = idUsuario.Value;
                //ML.Result result = BL.Usuario.UsuarioGetAllById(usuario);

                ML.Result result = new ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {

                        string urlApi = _configuration["urlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Usuario/GetById/" + idUsuario);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string usuarioCast = readTask.Result.Object.ToString();

                            ML.Usuario resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(usuarioCast);
                            result.Object = resultItem;
                            result.Correct = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;//unboxing

                    usuario.Rol.Roles = resultRol.Objects;

                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.idPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.idEstado);
                    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.idMunicipio);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    return View(usuario);

                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del usuario" + resultRol.ErrorMessage;
                    return View("Modal");
                }

            }
            else // MOSTRAR FORMULARIO AGREGAR NUEVO USUARIO
            {
                return View(usuario);
            }

        }

        [HttpPost] //Decorador que se utiliza cuando se quiere enviar informacion a la base desde el formulario
        public ActionResult Form(ML.Usuario usuario, int idUsuario)
        {
            //PARA IMAGEN
            IFormFile file = Request.Form.Files["ImgUsuario"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                usuario.Imagen = Convert.ToBase64String(imagen);
            }

            //ML.Result result = new ML.Result();

            //----------- PARA WEB SERVICE API ---------------------------------------------

            using (var client = new HttpClient())
            {
                if (usuario.idUsuario == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha registrado el usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registrado el usuario";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update/"+ idUsuario,usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registrado el usuario";
                        return PartialView("Modal");
                    }

                }

            }

            //------------------------------------------------------------------------------

            //if (ModelState.IsValid)
            //{

            //--------------------------------- PARA USAR BL ---------------------------------------------------
            //if (usuario.idUsuario > 0)
            //{
            //    //UPDATE
            //    result = BL.Usuario.UsuarioUpdate(usuario);
            //    ViewBag.Message = "Se ha actualizado el registro";
            //}
            //else
            //{
            //    //ADD                
            //    result = BL.Usuario.UsuarioAdd(usuario);
            //    ViewBag.Message = "Se ha agregadao el nuevo el registro";

            //}

            //if (result.Correct == true)
            //{

            //    return PartialView("Modal");
            //}
            //else
            //{
            //    return PartialView("Modal");
            //}
            //------------------------------ VALIDACIONES  ----------------------------------------------
            //}
            //else
            //{
            //    ML.Result resultRol = BL.Rol.RolGetAll();
            //    ML.Result resultPais = BL.Pais.PaisGetAll();

            //    usuario.Rol.Roles = resultRol.Objects;

            //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            //    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.idPais);
            //    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.idEstado);
            //    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.idMunicipio);
            //    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            //    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            //    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            //    return View();

            //}
        }

        //DELETE
        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.idUsuario = idUsuario.Value;

            //---------------------- DELETE SIN WEB SERVICE ------------------------
            //ML.Result resultDelete = BL.Usuario.UsuarioDelete(usuario);

            //if (resultDelete.Correct == true)
            //{
            //    ViewBag.Message = "Se ha eliminado el registro";
            //    return PartialView("Modal");
            //}
            //else
            //{
            //    ViewBag.Message = "No se ha eliminado el registro";
            //    return PartialView("Modal");
            //}
            //------------------------------------------------------------------------

            //---------------------- DELETE CON WEB SERVICE ------------------------
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //HTTP POST
                var postTask = client.GetAsync("Usuario/delete/" + idUsuario);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha eliminado el usuario";
                    return PartialView("Modal");
                }
            }
            //------------------------------------------------------------------------

        }

        [HttpPost]
        public JsonResult EstadoGetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();

            result = BL.Estado.EstadoGetByIdPais(idPais);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result resultMunicipio = new ML.Result();
            resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(idEstado);

            return Json(resultMunicipio.Objects);
        }

        [HttpPost]
        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result resultColonia = new ML.Result();
            resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(idMunicipio);

            return Json(resultColonia.Objects);

        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpPost]
        public JsonResult CambiarStatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(idUsuario, status);
            return Json(result);
        }

        //METODO PARA LOGIN USUARIO 

        [HttpGet]
        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUsuario(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetByUserName(usuario);

            if (result.Correct)
            {
                ML.Usuario usuarioUnbox = (ML.Usuario)result.Object;
                if (usuario.password == usuarioUnbox.password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "La contraseña es incorrecta";
                    return PartialView("Modal");
                    return View();
                }

            }
            else
            {
                ViewBag.Message = "El Nombre de Usuario es incorrecta o no existe";
                return PartialView("Modal");
            }

        }


    }
}
