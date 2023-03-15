using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultDep = BL.Departamento.DepartamentoGetAll();

            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();

            ML.Result result = BL.Producto.ProductoGetAll(producto);


            if (resultDep.Correct && result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Departamento.Departamentos = resultDep.Objects;
            }

            return View(producto);

        }

        [HttpGet]

        public ActionResult Form(int? idProducto)
        {
            ML.Result resultProv = BL.Proveedor.GetAll();
            ML.Result resultDep = BL.Departamento.DepartamentoGetAll();

            ML.Producto producto = new ML.Producto();

            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();

            if (resultProv.Correct && resultDep.Correct)
            {
                producto.Proveedor.Proveedores = resultProv.Objects;
                producto.Departamento.Departamentos = resultDep.Objects;
            }

            if (idProducto != null)
            {
                producto.IdProducto = idProducto.Value;
                ML.Result result = BL.Producto.GetById(producto);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Proveedor.Proveedores = resultProv.Objects;
                    producto.Departamento.Departamentos = resultDep.Objects;
                    return View(producto);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del usuario" + resultProv.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(producto);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            IFormFile file = Request.Form.Files["Img"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                producto.Imagen = Convert.ToBase64String(imagen);
            }


            ML.Result result = new ML.Result();

            if (producto.IdProducto > 0)
            {
                //UPDATE
                result = BL.Producto.Update(producto);
                ViewBag.Message = "Se ha actualizado el registro";
            }
            else
            {
                //ADD
                result = BL.Producto.Add(producto);
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
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


    }
}
