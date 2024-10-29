//using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculas.Entidades
{
    public class Actor
    {
        #region Campos
        private string nombre;
        #endregion



        #region Propiedades
        public int Id { get; set; }
        public string Nombre
        {
            get => nombre;
            set => nombre = string.Join(' ', value.Split(' ').Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray());
        }
        public string Biografia { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime? FechaNacimiento { get; set; }
        public HashSet<PeliculaActor> PeliculasActores { get; set; }
        #endregion
    }
}
