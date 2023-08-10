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

        private void OnCloseCommandBindingExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to close the Person Application?", "Person Application", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }
    }
}
