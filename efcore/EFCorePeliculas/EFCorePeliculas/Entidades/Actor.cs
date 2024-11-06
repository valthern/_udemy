//using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations.Schema;

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
        public string FotoURL { get; set; }
        [NotMapped]
        public int? Edad
        {
            get
            {
                if(!FechaNacimiento.HasValue) return null;

                var fechaNacimiento=FechaNacimiento.Value;
                var edad=DateTime.Today.Year-fechaNacimiento.Year;

                if (new DateTime(DateTime.Today.Year, fechaNacimiento.Month, fechaNacimiento.Day) > DateTime.Today) edad--;

                return edad;
            }
        }
        public Direccion Direccion { get; set; }
        #endregion
    }
}
