using PersonsDemo.Model;
using TinyCsvParser.Mapping;

namespace PersonsDemo.DataMapper
{
    public class CsvPersonMapping : CsvMapping<Person>
    {
        #region Constructor
        public CsvPersonMapping()
            : base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Country);
            MapProperty(2, x => x.Address);
            MapProperty(3, x => x.PostalZip);
            MapProperty(4, x => x.Email);
            MapProperty(5, x => x.Phone);
        }
        #endregion
    }
}
