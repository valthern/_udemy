using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCoreDatabaseFirst.Models;

[Table("usuario")]
public partial class usuario
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string email { get; set; } = null!;

    [StringLength(200)]
    public string nombre { get; set; } = null!;

    [StringLength(30)]
    public string? telefono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string password { get; set; } = null!;

    public int? es_admin { get; set; }
}
