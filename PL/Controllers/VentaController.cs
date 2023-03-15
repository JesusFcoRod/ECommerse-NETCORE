using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult ProductoGetAll()
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

        [HttpPost]
        public ActionResult ProductoGetAll(ML.Producto producto)
        {
            IFormFile file = Request.Form.Files["ImgProducto"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                producto.Imagen = Convert.ToBase64String(imagen);
            }


            ML.Result resultDep = BL.Departamento.DepartamentoGetAll();
            //producto.Departamento = new ML.Departamento();

            ML.Result result = BL.Producto.ProductoGetAll(producto);


            if (resultDep.Correct && result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Departamento.Departamentos = resultDep.Objects;
            }

            return View(producto);
        }

        //CARRITO
        public ActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CartPost(ML.Producto producto)
        {



            bool existe = false;
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentaProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                producto.Cantidad = producto.Cantidad = 1;
                producto.Subtotal = producto.PrecioUnitario * producto.Cantidad;
                ventaProducto.VentaProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                GetCarrito(ventaProducto);
                foreach (ML.Producto venta in ventaProducto.VentaProductos.ToList())
                {
                    if (producto.IdProducto == venta.IdProducto)
                    {
                        venta.Cantidad = venta.Cantidad + 1;
                        venta.Subtotal = venta.PrecioUnitario * venta.Cantidad;
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }

                    if (existe == true)
                    {
                        break;
                    }
                }
                if (existe == false)
                {
                    producto.Cantidad = producto.Cantidad = 1;
                    producto.Subtotal = producto.Cantidad * producto.PrecioUnitario;
                    ventaProducto.VentaProductos.Add(producto);
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.Message = "Se ha agregado el producto a tu carrito!";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo agregar el producto a tu carrito!";
                return PartialView("Modal");
            }

        }
        [HttpGet]
        public ActionResult ResumenCompra(ML.VentaProducto ventaProducto)
        {


            decimal costoTotal = 0;
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                ventaProducto.VentaProductos = new List<object>();
                GetCarrito(ventaProducto);
                ventaProducto.Total = costoTotal;
            }

            return View(ventaProducto);
        }

        public ML.VentaProducto GetCarrito(ML.VentaProducto ventaProducto)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

            foreach (var obj in ventaSession)
            {
                ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                ventaProducto.VentaProductos.Add(objProducto);
            }
            return ventaProducto;
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
