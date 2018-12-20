using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Dal.Abstract
{
    public interface IRepository<T> where T : DomainObject
    {
        List<T> GetAll();

        T Get(int id);

        void Save(T element);
    }
}
