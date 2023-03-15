using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result resultAdd = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext context = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"[ProductoAdd] '{producto.Nombre}',{producto.PrecioUnitario},{producto.Stock},{producto.Proveedor.IdProveedor},{producto.Departamento.idDepartamento},'{producto.Descripcion}','{producto.Imagen}'");
                    if (query > 0)
                    {
                        resultAdd.Correct = true;
                    }
                    else
                    {
                        resultAdd.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultAdd.Ex = ex;
                resultAdd.Correct = false;
                resultAdd.ErrorMessage = ex.Message;
            }
            return resultAdd;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result resultUpdate = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext context = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"[ProductoUpdate] {producto.IdProducto},'{producto.Nombre}',{producto.PrecioUnitario},{producto.Stock},{producto.Proveedor.IdProveedor},{producto.Departamento.idDepartamento},'{producto.Descripcion}','{producto.Imagen}'");
                    if (query > 0)
                    {
                        resultUpdate.Correct = true;
                    }
                    else
                    {
                        resultUpdate.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultUpdate.Ex = ex;
                resultUpdate.Correct = false;
                resultUpdate.ErrorMessage = ex.Message;
            }
            return resultUpdate;

        }
        public static ML.Result ProductoGetAll(ML.Producto producto)
        {
            ML.Result resultGetAll = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    //producto.Departamento = new ML.Departamento();

                    var query = contex.Productos.FromSqlRaw($"[ProductoGetAll] {producto.Departamento.idDepartamento}").ToList();

                    resultGetAll.Objects = new List<object>();

                    foreach (var objProd in query)
                    {
                        producto = new ML.Producto();

                        producto.IdProducto = objProd.IdProducto;
                        producto.Nombre = objProd.Nombre;
                        producto.PrecioUnitario = objProd.PrecioUnitario;
                        producto.Stock = objProd.Stock;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = objProd.IdProveedor.Value;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.idDepartamento = objProd.IdDepartamento.Value;
                        producto.Departamento.nombre = objProd.DepartamentoNombre;

                        producto.Descripcion = objProd.Descripcion;

                        producto.Imagen = objProd.Imagen;

                        resultGetAll.Objects.Add(producto);

                    }
                    resultGetAll.Correct = true;
                }
            }
            catch (Exception ex)
            {
                resultGetAll.Ex = ex;
                resultGetAll.Correct = false;
                resultGetAll.ErrorMessage = ex.Message;
            }
            return resultGetAll;
        }

        public static ML.Result GetById(ML.Producto producto)
        {
            ML.Result resultId = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Productos.FromSqlRaw($"[ProductoGetById] {producto.IdProducto}").AsEnumerable().FirstOrDefault();
                    ML.Producto productoGI = new ML.Producto();

                    productoGI.IdProducto = query.IdProducto;
                    productoGI.Nombre = query.Nombre;
                    productoGI.PrecioUnitario = query.PrecioUnitario;
                    productoGI.Stock = query.Stock;
                    productoGI.Descripcion = query.Descripcion;
                    productoGI.Imagen = query.Imagen;

                    productoGI.Proveedor = new ML.Proveedor();
                    productoGI.Proveedor.IdProveedor = int.Parse(query.IdProveedor.ToString());

                    productoGI.Departamento = new ML.Departamento();
                    productoGI.Departamento.idDepartamento = int.Parse(query.IdDepartamento.ToString());

                    resultId.Object = productoGI;
                    resultId.Correct = true;

                }
            }
            catch (Exception ex)
            {
                resultId.Ex = ex;
                resultId.Correct = false;
                resultId.ErrorMessage = ex.Message;
            }
            return resultId;

        }
    }
}
