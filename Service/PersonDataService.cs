using PersonsDemo.DataParser;
using PersonsDemo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonsDemo.Service
{
    public class PersonDataService : IPersonDataService
    {
        #region Private Members
        private IPersonCsvParser PersonCsvParser { get; set; }
        #endregion

        #region Contructor
        public PersonDataService(IPersonCsvParser personCsvParser)
        {
            PersonCsvParser = (PersonCsvParser)personCsvParser;
        }
        #endregion

        #region Public Methods
        public async Task<IList<Person>> GetPersons()
        {
            var result = await Task.Factory.StartNew(() => {
                var persons = PersonCsvParser.ParsePersons("Data/PersonsDemo.csv");
                return persons;
            });

            return result;
        }
        #endregion
    }
}
