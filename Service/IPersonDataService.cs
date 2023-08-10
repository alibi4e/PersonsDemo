using PersonsDemo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonsDemo.Service
{
    public interface IPersonDataService
    {
        Task<IList<Person>> GetPersons();
    }
}