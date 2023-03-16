using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public DepartamentoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            //----------- GET ALL SIN WEB SERVICE -------------------
            //ML.Departamento departamento = new ML.Departamento();

            //ML.Result result = BL.Departamento.DepartamentoGetAll();
            //departamento.Departamentos = result.Objects;

            //return View(departamento); 
            //--------------------------------------------------------

            //----------------GET ALL CON WEB SERVICE------------------

            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Departamento/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Departamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    departamento.Departamentos = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(departamento);


            //---------------------------------------------------------
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
                //ML.Result result = BL.Departamento.DepartamentoGetById(departamento);

                ML.Result result = new ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {

                        string urlApi = _configuration["urlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Departamento/GetById/" + idDepartamento);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string usuarioCast = readTask.Result.Object.ToString();

                            ML.Departamento resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(usuarioCast);
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
        public ActionResult Form(ML.Departamento departamento, int idDepartamento)
        {
            //-------------------- ADD Y UPDATE DEPARTAMENTO SIN WEB SERVICE ------
            //ML.Result result = new ML.Result();

            //if (departamento.idDepartamento > 0)
            //{
            //    //UPDATE
            //    result = BL.Departamento.DepartamentoUpdate(departamento);
            //    ViewBag.Message = "Se ha actualizado el registro";
            //}
            //else
            //{
            //    //ADD
            //    result = BL.Departamento.DepartamentoAdd(departamento);
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
            //---------------------------------------------------------------------------

            //-------------------- ADD Y UPDATE DEPARTAMENTO CON WEB SERVICE ------------

            using (var client = new HttpClient())
            {
                if (departamento.idDepartamento == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Departamento>("Departamento/Add", departamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha registrado el deparatamento";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registrado el departamento";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Departamento>("Departamento/Update/" + idDepartamento, departamento);
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

            //---------------------------------------------------------------------------
        }

        [HttpGet]
        public ActionResult Delete(int? idDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.idDepartamento = idDepartamento.Value;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //HTTP POST
                var postTask = client.GetAsync("Departamento/delete/" + idDepartamento);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado el departamento";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha eliminado el departamento";
                    return PartialView("Modal");
                }
            }
        }

    }
}
