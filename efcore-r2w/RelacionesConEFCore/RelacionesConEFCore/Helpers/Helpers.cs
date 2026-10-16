using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Helpers;

public static class Helpers
{
    public static void ManejoDeDatos(ApplicationDbContext context)
    {
        //SiembraDeDatos(context);
        MuestraDeDatos(context);
        //ModificacionDeDatos(context);
    }

    private static void ModificacionDeDatos(ApplicationDbContext context)
    {
        //// Buscar el primer artículo con su categoría
        //var articulo = context.Articulos
        //    .Include(a => a.Categoria)
        //    .First();

        //Console.WriteLine($"Antes: {articulo.Titulo}, Categoría: {articulo.Categoria.Nombre}");

        //// Cambiar título y categoría
        //articulo.Titulo = "Novedades de EF Core 10 - Tutorial Avanzado";
        //articulo.Categoria.Nombre = "Bases de Datos";

        //context.SaveChanges();
        //Console.WriteLine("Artículo y categoría actualizados.");

        //var usuario = context.Usuarios
        //    .Include(u => u.Perfil)
        //    .First();

        //Console.WriteLine($"Antes: {usuario.Nombre}, Categoría: {usuario.Perfil.Biografia}");

        //// Cambiar los datos
        //usuario.Nombre = "Carlos Actualizado";
        //usuario.Perfil.Biografia = "Arquitecto de software con experiencia en .NET y EF Core. Actualizado.";

        //context.SaveChanges();
        //Console.WriteLine($"Usuario {usuario.Nombre}, Biografía: {usuario.Perfil.Biografia}");
        //Console.WriteLine("Usuario y perfilactualizados correctamente.");

        //var articuloConComentarios = context.Articulos
        //    .Include(a => a.Comentarios)
        //    .First();

        //Console.WriteLine($"Articulo: {articuloConComentarios.Titulo}");

        //// Cambiar el texto del primer comentario
        //var primerComentario = articuloConComentarios.Comentarios.First();
        //primerComentario.Texto = "Comentario editado por el autor.";

        //context.SaveChanges();

        //Console.WriteLine("Comentario actualizado.");

        //var articuloConEtiquetas = context.Articulos
        //    .Include(a => a.Etiquetas)
        //    .First();

        //Console.WriteLine($"Antes: {string.Join(", ", articuloConEtiquetas.Etiquetas.Select(e => e.Nombre))}");

        //var etiquetaQuitar = articuloConEtiquetas.Etiquetas.First();
        //articuloConEtiquetas.Etiquetas.Remove(etiquetaQuitar);

        //var nuevaEtiqueta = new Etiqueta { Nombre = "DataAnnotations" };
        //articuloConEtiquetas.Etiquetas.Add(nuevaEtiqueta);

        //context.SaveChanges();

        //Console.WriteLine($"Después: {string.Join(", ", articuloConEtiquetas.Etiquetas.Select(e => e.Nombre))}");

        //var articulo = context.Articulos
        //    .Include(a => a.Comentarios)
        //    .First();

        //Console.WriteLine($"Eliminando artículo: {articulo.Titulo} con {articulo.Comentarios.Count} comentarios...");

        //context.Articulos.Remove(articulo);
        //context.SaveChanges();

        //Console.WriteLine("Artículo y sus comentarios eliminados (cascade delete).");

        //var usuario = context.Usuarios
        //    .Include(u => u.Perfil)
        //    .First();

        //Console.WriteLine($"Eliminando usuario: {usuario.Nombre} con perfil asociado...");

        //context.Usuarios.Remove(usuario);
        //context.SaveChanges();

        //Console.WriteLine("usuario y perfil eliminados (cascade delete).");

        //var etiqueta = context.Etiquetas
        //    .Include(e => e.Articulos)
        //    .First();

        //Console.WriteLine($"Eliminando etiqueta: {etiqueta.Nombre}");

        //context.Etiquetas.Remove(etiqueta);
        //context.SaveChanges();

        //Console.WriteLine("Etiqueta eliminada y relaciones en tabla intermedia borradas.");
    }

    private static void MuestraDeDatos(ApplicationDbContext context)
    {
        Console.WriteLine("=== lectura de datos con relaciones ===");

        var articulos = context.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Etiquetas)
            .Include(a => a.Comentarios)
                .ThenInclude(c => c.Usuario)
                    .ThenInclude(u => u.Perfil)
            .ToList();

        foreach (var articulo in articulos)
        {
            Console.WriteLine($" Artículo: {articulo.Titulo}");
            Console.WriteLine($" Categoría: {articulo.Categoria.Nombre}");
            Console.WriteLine($" Etiquetas: {string.Join(", ", articulo.Etiquetas.Select(e => e.Nombre))}");

            foreach (var comentario in articulo.Comentarios)
            {
                Console.WriteLine($"  Comentario: {comentario.Texto} (Por: {comentario.Usuario.Nombre})");
                Console.WriteLine($"  Perfil del usuario: {comentario.Usuario.Perfil?.Biografia}");
            }
        }
    }

    private static void SiembraDeDatos(ApplicationDbContext context)
    {
        //Aquí puedes agregar la lógica para sembrar datos en la base de datos utilizando el contexto proporcionado.

        // Asegúrate de que la base de datos esté creada antes de insertar datos.
        // Para ello, puedes usar el método EnsureCreated() del contexto de la base de datos.
        context.Database.EnsureCreated();

        // === Inserción de Categorías ===
        var categoria = new Categoria { Nombre = "Programación" };

        // === inserción de Etiquetas ===
        var etiqueta1 = new Etiqueta { Nombre = "EF Core" };
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

        // Agregar todo al contexto
        context.Categorias.Add(categoria);
        context.Etiquetas.AddRange(etiqueta1, etiqueta2);
        context.Usuarios.Add(usuario);
        context.Articulos.Add(articulo);
        context.Comentarios.AddRange(comentario1, comentario2);

        // Guardar los cambios en la base de datos
        context.SaveChanges();

        Console.WriteLine("Datos insertados correctamente en la base de datos.");
    }
}
