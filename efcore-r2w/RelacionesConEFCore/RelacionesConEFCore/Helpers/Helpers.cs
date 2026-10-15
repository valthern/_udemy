using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Helpers;

public static class Helpers
{
    public static void SiembraDeDatos(ApplicationDbContext context)
    {
        // Aquí puedes agregar la lógica para sembrar datos en la base de datos utilizando el contexto proporcionado.

        // Asegúrate de que la base de datos esté creada antes de insertar datos.
        // Para ello, puedes usar el método EnsureCreated() del contexto de la base de datos.
        context.Database.EnsureCreated();

        // === Inserción de Categorías ===
        var categoria=new Categoria { Nombre= "Programación" };

        // === inserción de Etiquetas ===
        var etiqueta1=new Etiqueta { Nombre= "EF Core" };
        var etiqueta2 = new Etiqueta { Nombre = ".Net 10" };

        // === Inserción de Usuario ===
        var usuario = new Usuario { Nombre = "Carlos", Email = "carlos@blog.com" };

        // === Inserción de Perfil del Usuario ===
        usuario.Perfil = new PerfilUsuario 
        {
            Biografia = "Desarrollador de software con experiencia en .NET y EF Core.", 
            FotoUrl = "https://example.com/foto.jpg" 
        };

        // === Inserción de Artículo ===
        var articulo = new Articulo 
        { 
            Titulo = "Novedades de EF Core 10", 
            Contenido = "EF Core es un ORM para .NET que permite trabajar con bases de datos de manera más sencilla.",
            Categoria = categoria,
            Etiquetas = new List<Etiqueta> { etiqueta1, etiqueta2 }
        };

        // === Inserción de Comentarios ===
        var comentario1 = new Comentario
        {
            Texto = "muy buen artículo, muy informativo.",
            Usuario = usuario,
            Articulo = articulo
        };

        var comentario2 = new Comentario
        {
            Texto = "Tengo una duda con las migraciones.",
            Usuario = usuario,
            Articulo = articulo
        };
    }
}
