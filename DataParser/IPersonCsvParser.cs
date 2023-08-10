using PersonsDemo.Model;
using System.Collections.Generic;

namespace PersonsDemo.DataParser
{
    public interface IPersonCsvParser
    {
        List<Person> ParsePersons(string fileName);
    }
}