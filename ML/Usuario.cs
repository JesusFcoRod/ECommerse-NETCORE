using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        //Propiedades de la tabla o atributos
        public int idUsuario { get; set; }

        //[Required]
        public string userName { get; set; }

        //[Required]
        //[RegularExpression("^([A-ZÁÉÍÓÚÑa-zñáéíóúñ]{1,}'?-?[A-ZÁÉÍÓÚÑa-zñáéíóú]+[\\s]*)+$")]
        public string nombre { get; set; }

        //[Required]
        //[RegularExpression("^([A-ZÁÉÍÓÚÑa-zñáéíóúñ]{1,}'?-?[A-ZÁÉÍÓÚÑa-zñáéíóú]+[\\s]*)+$")]
        public string apellidoPaterno { get; set; }

        //[RegularExpression("^([A-ZÁÉÍÓÚÑa-zñáéíóúñ]{1,}'?-?[A-ZÁÉÍÓÚÑa-zñáéíóú]+[\\s]*)+$")]
        public string apellidoMaterno { get; set; }

        //[Required]
        //[RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string email { get; set; }

        [Required]
        //PARA PASSWORD
        //1.- Tiene una extensión mínima de 8 caracteres (modificando {8})
        //2.- Al menos una letra mayúscula (?=.*?[A-Z])
        //3.- Al menos una letra minúscula (?=.*?[a - z])
        //4.- Al menos un dígito (?=.*?[0 - 9])
        public string password { get; set; }

        //[Required]
        public string fechaNacimiento { get; set; }

        //[Required]
        public string sexo { get; set; }
        public string curp { get; set; }

        //[Required]
        public string telefono { get; set; }

        //[RegularExpression("^\\+?[1-9][0-9]{7,14}$")]
        public string celular { get; set; }


        public string nombreCompleto { get; set; }
        public string direccionCompleta { get; set; }
        public string contacto { get; set; }
        public string DatosGenerales { get; set; }

        public string nombreRol { get; set; }
        public string Imagen { get; set; }

        public bool Status { get; set; }

        //Propiedades de navegacion para FK 
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }

        //LISTA PARA EXTRAER LOS OBJETOS Y MOSTRAR EN TABAL WEB
        public List<object> Usuarios { get; set; }
    }
}
