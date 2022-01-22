using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public interface IRepository<T>
    {
        T Add(T Tobj);
        Task Set(T Tobj);
        T Get(T obj);
        Task<T> Get(string id);
        T Delete(T obj);
        IEnumerable<T> GetAll();
    }
}
