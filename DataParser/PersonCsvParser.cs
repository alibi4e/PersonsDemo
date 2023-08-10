using PersonsDemo.DataMapper;
using PersonsDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace PersonsDemo.DataParser
{
    public class PersonCsvParser : IPersonCsvParser
    {
        #region Public Methods
        public List<Person> ParsePersons(string fileName)
        {
            try
            {
                var csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
                var csvParserOptions = new CsvParserOptions(true, ',');
                var csvMapper = new CsvPersonMapping();
                var csvParser = new CsvParser<Person>(csvParserOptions, csvMapper);
                return csvParser.ReadFromFile(fileName, Encoding.ASCII).Select(item => item.Result).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<Person>();
        }
        #endregion
    }
}
