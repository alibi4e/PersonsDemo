using PersonsDemo.DataMapper;
using PersonsDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace PersonsDemo.Service
{
    public class PersonDataService : IPersonDataService
    {
        public async Task<IList<Person>> GetPersons()
        {
            var result = await Task.Factory.StartNew(() => {
                var csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
                var csvParserOptions = new CsvParserOptions(true, ',');
                var csvMapper = new CsvPersonMapping();
                var csvParser = new CsvParser<Person>(csvParserOptions, csvMapper);
                var persons = csvParser.ReadFromFile("Data/PersonsDemo.csv", Encoding.ASCII).Select(item => item.Result).ToList();
                return persons;
            });

            return result;
        }
    }
}
