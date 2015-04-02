using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace torsion.Domain.Repository
{
    public interface IBaseRepository<T>
     where T : class
    {
        bool Add(T entity);
        bool Delete(T entity);
        bool Delete(int id);
        T Find(int id);
        IQueryable<T> Load(Func<T, bool> whereLambda);
        IQueryable<T> LoadPage<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
        bool Update(T entity);
    }
}
