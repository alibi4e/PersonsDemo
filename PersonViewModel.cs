using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonsDemo.Model;
using PersonsDemo.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace PersonsDemo
{
    public partial class PersonViewModel : ObservableObject
    {
        [ObservableProperty]
        public ICollectionView? _personsView;

        [ObservableProperty]
        public Dictionary<string, object>? _countryList;

        [ObservableProperty]
        public Dictionary<string, object>? _selectedCountries = new();

        public List<string>? SortFields { get; set; } = new() { "Name", "Country" };

        private IPersonDataService? PersonDataService { get; set; } = null;

        public ObservableCollection<Person>? Persons { get; set; }

        public ICommand? PersonWindowLoadedCommand { get; set; }

        public ICommand? SortCommand { get; set; }

        public ICommand? CountrySelectedCommand { get; set; }

        public PersonViewModel(IPersonDataService personDataService)
        {
            PersonDataService = personDataService;
            PersonWindowLoadedCommand = new AsyncRelayCommand(PersonWindowLoaded);
            SortCommand = new RelayCommand<string>(Sort);
            CountrySelectedCommand = new RelayCommand(CountrySelected);
        }

        private void CountrySelected()
        {
            PersonsView?.Refresh();
        }

        private bool FilterByCountry(object obj)
        {
            if (SelectedCountries == null)
            {
                return true;
            }

            if (obj is not Person person)
            {
                return false;
            }

            return SelectedCountries.ContainsKey(person.Country ?? string.Empty); ;            
        }

        private void Sort(string? sortByField)
        {
            PersonsView?.SortDescriptions.Clear();
            PersonsView?.SortDescriptions.Add(new SortDescription(sortByField, ListSortDirection.Ascending));
        }

        private async Task PersonWindowLoaded()
        {
            if (PersonDataService != null)
            {
                var persons = await PersonDataService.GetPersons();
                Persons = new ObservableCollection<Person>(persons);
                CountryList = GetCountryList();
                PersonsView = CollectionViewSource.GetDefaultView(Persons);
                PersonsView.Filter = FilterByCountry;
            }
        }

        private Dictionary<string, object>? GetCountryList()
        {
            var uniqueCountryList = Persons?.Select(item => new KeyValuePair<string, object>(item.Country ?? string.Empty, item)).GroupBy(x => x.Key).Select(g => g.First()).ToList();
            return uniqueCountryList?.ToDictionary(x => x.Key, x => x.Value);
        }
     }
}
