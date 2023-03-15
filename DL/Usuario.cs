using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string UserName { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public int? IdRol { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string? Imagen { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Rol? IdRolNavigation { get; set; }

    //ALIAS EN STORED GET ALL
    public string NombreRol { get; set; }
    public string Direccioncalle { get; set; }
    public string DireccionNumInt { get; set; }
    public string DireccionNumExt { get; set; }
    public string nombreColonia { get; set; }
    public string nombreMunicipio { get; set; }
    public string nombreEstado { get; set; }
    public string nombrePais { get; set; }

    //ALIAS EN STORED ADD
    public int ColoniaId { get; set; }
    public int PaisId { get; set; }
    public int EstadoId { get; set; }
    public int MunicipioId { get; set; }
}
