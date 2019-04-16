using Richard2.Models;
using System.Collections.Generic;

namespace Richard2.Repository
{
    public interface IRepository_file<T> where T : BaseEntity
    {     
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();       

    }
}
