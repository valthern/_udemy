using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //protected readonly ApplicationDbContext context;
        private readonly ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity) => dbSet.Add(entity);

        public T Get(int id) => dbSet.Find(id);

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet swl contexto
            IQueryable<T> query = dbSet;

            // Se aplica el filtro a la consulta si se proporciona
            if (filter is not null) query = query.Where(filter);

            // Se incluyen las propiedades relacionadas si se proporcionan
            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            // Se ordena la consulta si se proporciona una función de ordenación
            if (orderby is not null) return orderby(query).ToList();

            // Si no se proporciona una función de ordenación, se devuelve la lista sin ordenar
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet swl contexto
            IQueryable<T> query = dbSet;

            // Se aplica el filtro a la consulta si se proporciona
            if (filter is not null) query = query.Where(filter);

            // Se incluyen las propiedades relacionadas si se proporcionan
            if (includeProperties is not null)
            {
                //foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            // Si no se proporciona una función de ordenación, se devuelve la lista sin ordenar
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T? entity = dbSet.Find(id);
            //if (entity is not null) dbSet.Remove(entity);
        }

        public void Remove(T entity)
        {
            //if (entity is not null) dbSet.Remove(entity);
            dbSet.Remove(entity);
        }
    }
}
