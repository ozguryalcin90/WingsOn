using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Dal.Abstract
{
    public interface IPersonRepository : IRepository<Person> 
    {
        IEnumerable<Person> GetByGender(GenderType gender);
    }
}