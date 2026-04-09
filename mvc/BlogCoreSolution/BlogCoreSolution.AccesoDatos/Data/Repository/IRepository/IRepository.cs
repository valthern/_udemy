using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string? includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
        );

        void Add(T entity);

        //void Update(T entity);

        void Delete(int id);

        void Delete(T entity);
    }
}
