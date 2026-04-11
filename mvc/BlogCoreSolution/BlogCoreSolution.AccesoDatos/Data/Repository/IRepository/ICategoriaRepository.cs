using BlogCoreSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);
    }
    /*
     * ¿Por qué el método Update no se encuentra en la interfaz IRepository<T> y sí en la interfaz ICategoriaRepository?
     * Cada entidad se actualiza de forma diferente, dependiendo de sus propiedades y de cómo se maneje la actualización 
     * en el contexto de datos. Al definir el método Update en la interfaz específica de la entidad (ICategoriaRepository) 
     * se permite una implementación personalizada para esa entidad en particular, lo que puede ser necesario para manejar 
     * casos específicos de actualización que no se aplican a todas las entidades.
     * 
     * Categoría: Nombre, Orden
     * Post: Título, Contenido, Imagen, Fecha
     * Usuario: "Datos distintos"
     * 
     * No todas las propiedades se deben actualizar. Algunas se ignoran, otras se claculan, otras son inmutables, etc.
     * Realizar una actualización en cada interfaz correspondiente a cada entidad permite una mayor flexibilidad y control 
     * sobre cómo se manejan las actualizaciones para cada tipo de entidad. Update(entity) genérico puede marcar todo como 
     * modificado, y se corre el riesgo de actualizar propiedades que no se desean actualizar, lo que puede llevar a 
     * errores graves.
     */
}
