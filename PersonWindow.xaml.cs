using System.Windows;

namespace PersonsDemo
{
    public partial class PersonWindow : Window
    {
        public PersonViewModel? ViewModel { get; set; }
        public PersonWindow(PersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
