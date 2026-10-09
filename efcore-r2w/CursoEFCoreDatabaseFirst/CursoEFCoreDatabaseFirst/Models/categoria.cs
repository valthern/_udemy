using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCoreDatabaseFirst.Models;

[Table("categoria")]
public partial class categoria
{
    [Key]
    public int idcategoria { get; set; }

    [StringLength(100)]
    public string nombre { get; set; } = null!;

    [StringLength(250)]
    public string? descripcion { get; set; }

    public bool activo { get; set; }
}
