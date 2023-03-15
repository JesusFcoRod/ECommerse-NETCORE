using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Drawing2D;

namespace BL
{
    public class Usuario
    {
        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result resultAdd = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {                   
                    var query = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.userName}','{usuario.nombre}','{usuario.apellidoPaterno}','{usuario.apellidoMaterno}','{usuario.email}', '{usuario.password}', '{usuario.fechaNacimiento}','{usuario.sexo}','{usuario.telefono}','{usuario.celular}','{usuario.curp}',{usuario.Rol.idRol},'{usuario.Imagen}','{usuario.Direccion.calle}','{usuario.Direccion.numeroInterio}','{usuario.Direccion.numeroExterior}',{usuario.Direccion.Colonia.idColonia}");

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

        public static ML.Result UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Result resultUpdate = new Result();
            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.userName}', '{usuario.nombre}','{usuario.apellidoPaterno}', '{usuario.apellidoMaterno}','{usuario.email}', '{usuario.password}', '{usuario.fechaNacimiento}', '{usuario.sexo.ToString()}', '{usuario.telefono}', '{usuario.celular}', '{usuario.curp}','{usuario.Rol.idRol}',{usuario.idUsuario},'{usuario.Imagen}','{usuario.Direccion.calle}','{usuario.Direccion.numeroInterio}','{usuario.Direccion.numeroExterior}', '{usuario.Direccion.Colonia.idColonia}'");

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

        public static ML.Result UsuarioDelete(ML.Usuario usuario)
        {
            ML.Result resultDelete = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[UsuarioUDelete] {usuario.idUsuario}");

                    if (query > 0)
                    {
                        resultDelete.Correct = true;
                    }
                    else
                    {
                        resultDelete.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultDelete.Ex = ex;
                resultDelete.Correct = false;
                resultDelete.ErrorMessage = ex.Message;
            }
            return resultDelete;
        }

        public static ML.Result UsuarioGetAll(ML.Usuario usuario)
        {
            ML.Result resultGetlALL = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.nombre}','{usuario.apellidoPaterno}','{usuario.apellidoMaterno}'").ToList();

                    resultGetlALL.Objects = new List<object>();

                    foreach (var objetoUsuario in query)
                    {
                        usuario = new ML.Usuario();

                        usuario.idUsuario = int.Parse(objetoUsuario.IdUsuario.ToString());
                        usuario.userName = objetoUsuario.UserName;

                        usuario.nombreCompleto = objetoUsuario.Nombre + " " +
                                                 objetoUsuario.ApellidoPaterno + " " +
                                                 objetoUsuario.ApellidoMaterno;

                        
                        usuario.password = objetoUsuario.Password;

                        usuario.DatosGenerales = "Fecha de Nacimiento: " + objetoUsuario.FechaNacimiento.ToString("dd-MM-yyyy") + " " +
                                                 "CURP: " + objetoUsuario.Curp + " " +
                                                 "Genero: " + objetoUsuario.Sexo;


                        usuario.contacto = "Telefono: " + objetoUsuario.Telefono +" "+
                                           "Celular: " + objetoUsuario.Celular + " " +
                                           "Email: " + objetoUsuario.Email;


                        usuario.Rol = new ML.Rol();
                        usuario.Rol.idRol = objetoUsuario.IdRol.Value;
                        usuario.Rol.descripcion = objetoUsuario.NombreRol;

                        usuario.direccionCompleta = "Calle: " + objetoUsuario.Direccioncalle + " " +
                                                    "No.Exterior: " + objetoUsuario.DireccionNumExt + " " +
                                                    "No.Interior: " + objetoUsuario.DireccionNumInt + " " +
                                                    "Colonia: " + objetoUsuario.nombreColonia + " " +
                                                    "Municipio: " + objetoUsuario.nombreMunicipio + " " +
                                                    "Estado: " + objetoUsuario.nombreEstado + " " +
                                                    "Pais: " + objetoUsuario.nombrePais;

                        usuario.Imagen = objetoUsuario.Imagen;

                        usuario.Status = objetoUsuario.Status.Value;


                        resultGetlALL.Objects.Add(usuario);
                    }
                    resultGetlALL.Correct = true;

                }

            }
            catch (Exception ex)
            {
                resultGetlALL.Ex = ex;
                resultGetlALL.Correct = false;
                resultGetlALL.ErrorMessage = ex.Message;
            }
            return resultGetlALL;
        }

        public static ML.Result UsuarioGetAllById(ML.Usuario usuario)
        {
            ML.Result resultById = new ML.Result();
            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"UsuarioGetById {usuario.idUsuario}").AsEnumerable().FirstOrDefault();
                    ML.Usuario usuarioGetId = new ML.Usuario();

                    usuarioGetId.idUsuario = query.IdUsuario;
                    usuarioGetId.userName = query.UserName;
                    usuarioGetId.nombre = query.Nombre;
                    usuarioGetId.apellidoPaterno = query.ApellidoPaterno;
                    usuarioGetId.apellidoMaterno = query.ApellidoMaterno;
                    usuarioGetId.email = query.Email;
                    usuarioGetId.password = query.Password;
                    usuarioGetId.fechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                    usuarioGetId.sexo = query.Sexo;
                    usuarioGetId.telefono = query.Telefono;
                    usuarioGetId.celular = query.Celular;
                    usuarioGetId.curp = query.Curp;
                    usuarioGetId.Imagen = query.Imagen;

                    usuarioGetId.Rol = new ML.Rol();
                    usuarioGetId.Rol.idRol = query.IdRol.Value;
                    usuarioGetId.Rol.descripcion = query.NombreRol;

                    usuarioGetId.Direccion = new ML.Direccion();
                    usuarioGetId.Direccion.calle = query.Direccioncalle;
                    usuarioGetId.Direccion.numeroExterior = query.DireccionNumExt;
                    usuarioGetId.Direccion.numeroInterio = query.DireccionNumInt;

                    usuarioGetId.Direccion.Colonia = new ML.Colonia();
                    usuarioGetId.Direccion.Colonia.idColonia = query.ColoniaId;

                    usuarioGetId.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuarioGetId.Direccion.Colonia.Municipio.idMunicipio = query.MunicipioId;

                    usuarioGetId.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuarioGetId.Direccion.Colonia.Municipio.Estado.idEstado = query.EstadoId;

                    usuarioGetId.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    usuarioGetId.Direccion.Colonia.Municipio.Estado.Pais.idPais = query.PaisId;

                    //Boxing: Guardar el modelo o tipo de dato en un object
                    resultById.Object = usuarioGetId;

                    resultById.Correct = true;
                }
            }
            catch (Exception ex)
            {
                resultById.Ex = ex;
                resultById.Correct = false;
                resultById.ErrorMessage = ex.Message;
            }
            return resultById;

        }

        public static ML.Result ChangeStatus(int idUsuario, bool status)
        {
            ML.Result resulStatus = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[UsuarioChangeStatus] '{idUsuario}', '{status}'");

                    if (query > 0)
                    {
                        resulStatus.Correct = true;
                    }
                    else
                    {
                        resulStatus.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resulStatus.Ex = ex;
                resulStatus.Correct = false;
                resulStatus.ErrorMessage = ex.Message;
            }
            return resulStatus;
        }

        public static ML.Result GetByUserName(ML.Usuario usuario)
        {
            ML.Result resultUserName = new ML.Result();

            try
            {
                using (DL.JrodriguezProgramacionNcapasContext contex = new DL.JrodriguezProgramacionNcapasContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"[UsuarioGetByUserName] '{usuario.userName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuarioLogin = new ML.Usuario();

                        usuarioLogin.userName = query.UserName;
                        usuarioLogin.password = query.Password;

                        resultUserName.Object = usuarioLogin;
                        resultUserName.Correct = true;
                    }
                    else
                    {
                        resultUserName.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultUserName.Ex = ex;
                resultUserName.Correct = false;
                resultUserName.ErrorMessage = ex.Message;
            }
            return resultUserName;

        }

        public static ML.Result ConvertXSLXtoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.userName = row[0].ToString();
                                usuario.nombre = row[1].ToString();
                                usuario.apellidoPaterno = row[2].ToString();
                                usuario.apellidoMaterno = row[3].ToString();
                                usuario.email = row[4].ToString();
                                usuario.password = row[5].ToString();
                                usuario.sexo = row[6].ToString();
                                usuario.telefono = row[7].ToString();
                                usuario.celular = row[8].ToString();
                                usuario.curp = row[9].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.idRol = int.Parse(row[10].ToString());
                                usuario.fechaNacimiento = row[11].ToString();
                                usuario.Status = bool.Parse(row[12].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.calle = row[13].ToString();
                                usuario.Direccion.numeroExterior = row[14].ToString();
                                usuario.Direccion.numeroInterio = row[15].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.idColonia = int.Parse(row[16].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;

                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();

                    error.IdRegistro = i++;

                    if (usuario.userName == "")
                    {
                        error.Mensaje += "Ingresar el UserName  ";
                    }
                    if (usuario.nombre == "")
                    {
                        error.Mensaje += "Ingresar el Nombre  ";
                    }
                    if (usuario.apellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paternos  ";
                    }
                    if (usuario.apellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno  ";
                    }
                    if (usuario.email == "")
                    {
                        error.Mensaje += "Ingresar el Email  ";
                    }
                    if (usuario.password == "")
                    {
                        error.Mensaje += "Ingresar el Password  ";
                    }
                    if (usuario.sexo == "")
                    {
                        error.Mensaje += "Ingresar el Sexo  ";
                    }
                    if (usuario.telefono == "")
                    {
                        error.Mensaje += "Ingresar el Telefono  ";
                    }
                    if (usuario.celular == "")
                    {
                        error.Mensaje += "Ingresar el Celular  ";
                    }
                    if (usuario.curp == "")
                    {
                        error.Mensaje += "Ingresar el CURP  ";
                    }
                    if (usuario.Rol.idRol == null)
                    {
                        error.Mensaje += "Ingresar el id Rol  ";
                    }
                    if (usuario.fechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la Fecha de Nacimiento  ";
                    }
                    if (usuario.Status == null)
                    {
                        error.Mensaje += "Ingresar el Status  ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                    if (usuario.Direccion.calle == null)
                    {
                        error.Mensaje += "Ingresar la Calle  ";
                    }
                    if (usuario.Direccion.numeroExterior == null)
                    {
                        error.Mensaje += "Ingresar el Numero Exterior  ";
                    }
                    if (usuario.Direccion.numeroInterio == null)
                    {
                        error.Mensaje += "Ingresar el Numero Interior  ";
                    }
                    if (usuario.Direccion.Colonia.idColonia == null)
                    {
                        error.Mensaje += "Ingresar el id de Colonia  ";
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}