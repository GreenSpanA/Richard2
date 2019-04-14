using Richard2.Models;
using System.Collections.Generic;

namespace Richard2.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();

        //TODO
        IEnumerable<T> FindCurrent(string size_comment);

    }
}
