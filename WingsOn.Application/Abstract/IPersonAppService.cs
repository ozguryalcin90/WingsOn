using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Application.Abstract
{
    public interface IPersonAppService
    {
        Person Get(int id);
        IEnumerable<Person> GetByGender(string gender);
    }
}