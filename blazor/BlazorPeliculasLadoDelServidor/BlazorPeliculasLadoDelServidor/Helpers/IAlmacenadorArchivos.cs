namespace BlazorPeliculasLadoDelServidor.Helpers
{
    public interface IAlmacenadorArchivos
    {
        // NOTA: Los contendedores en Azure, son como carpetas en el sistema de archivos.
        Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor);

        Task EliminarArchivo(string ruta, string NombreContenedor);
        
        async Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string ruta)
        {
            if(ruta is not null)
                await EliminarArchivo(ruta, nombreContenedor);

            return await GuardarArchivo(contenido, extension, nombreContenedor);
        }
    }
}
