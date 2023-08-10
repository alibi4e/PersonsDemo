using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonsDemo;

/// <summary>
/// Interaction logic for MultiSelectComboBox.xaml
/// </summary>
public partial class MultiSelectComboBox : UserControl
{
    #region Private Members
    private ObservableCollection<Node> _nodeList;
    #endregion

    #region Constructor
    public MultiSelectComboBox()
    {
        InitializeComponent();
        _nodeList = new ObservableCollection<Node>();
    }
    #endregion

    #region Dependency Properties

    public static readonly DependencyProperty ItemsSourceProperty =
         DependencyProperty.Register("ItemsSource", typeof(Dictionary<string, object>), typeof(MultiSelectComboBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(MultiSelectComboBox.OnItemsSourceChanged)));

    public static readonly DependencyProperty SelectedItemsProperty =
     DependencyProperty.Register("SelectedItems", typeof(Dictionary<string, object>), typeof(MultiSelectComboBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(MultiSelectComboBox.OnSelectedItemsChanged)));

    public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register("Text", typeof(string), typeof(MultiSelectComboBox), new UIPropertyMetadata(string.Empty));

    public static readonly DependencyProperty DefaultTextProperty =
        DependencyProperty.Register("DefaultText", typeof(string), typeof(MultiSelectComboBox), new UIPropertyMetadata(string.Empty));

    public ICommand ItemsSelectedCommand
    {
        get { return (ICommand)GetValue(ItemsSelectedCommandProperty); }
        set { SetValue(ItemsSelectedCommandProperty, value); }
    }   
    
    public static readonly DependencyProperty ItemsSelectedCommandProperty =
        DependencyProperty.Register(nameof(ItemsSelectedCommand), typeof(ICommand), typeof(MultiSelectComboBox), new PropertyMetadata(default(ICommand)));

    public Dictionary<string, object> ItemsSource
    {
        get { return (Dictionary<string, object>)GetValue(ItemsSourceProperty); }
        set
        {
            SetValue(ItemsSourceProperty, value);
        }
    }

    public Dictionary<string, object> SelectedItems
    {
        get { return (Dictionary<string, object>)GetValue(SelectedItemsProperty); }
        set
        {
            SetValue(SelectedItemsProperty, value);
        }
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public string DefaultText
    {
        get { return (string)GetValue(DefaultTextProperty); }
        set { SetValue(DefaultTextProperty, value); }
    }
    #endregion

    #region Events
    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        MultiSelectComboBox control = (MultiSelectComboBox)d;
        control.DisplayInControl();
    }

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        MultiSelectComboBox control = (MultiSelectComboBox)d;
        control.SelectNodes();
        control.SetText();
    }

    private void CheckBox_Click(object sender, RoutedEventArgs e)
    {
        CheckBox clickedBox = (CheckBox)sender;

        if (clickedBox.Content as string == "All")
        {
            if (clickedBox.IsChecked.Value)
            {
                foreach (Node node in _nodeList)
                {
                    node.IsSelected = true;
                }
            }
            else
            {
                foreach (Node node in _nodeList)
                {
                    node.IsSelected = false;
                }
            }
        }
        else
        {
            int _selectedCount = 0;
            foreach (Node s in _nodeList)
            {
                if (s.IsSelected && s.Title != "All")
                    _selectedCount++;
            }
            if (_selectedCount == _nodeList.Count - 1)
                _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = true;
            else
                _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = false;
        }

        SetSelectedItems();
        SetText();
        ItemsSelectedCommand?.Execute(Text);
    }
    #endregion

    #region Methods
    private void SelectNodes()
    {
        foreach (KeyValuePair<string, object> keyValue in SelectedItems)
        {
            Node node = _nodeList.FirstOrDefault(i => i.Title == keyValue.Key);
            if (node != null)
                node.IsSelected = true;
        }
    }

    private void SetSelectedItems()
    {
        if (SelectedItems == null)
            SelectedItems = new Dictionary<string, object>();
        SelectedItems.Clear();
        foreach (Node node in _nodeList)
        {
            if (node.IsSelected && node.Title != "All")
            {
                if (this.ItemsSource.Count > 0)

                    SelectedItems.Add(node.Title, this.ItemsSource[node.Title]);
            }
        }
    }

    private void DisplayInControl()
    {
        _nodeList.Clear();
        if (this.ItemsSource.Count > 0)
            _nodeList.Add(new Node("All"));
        foreach (KeyValuePair<string, object> keyValue in this.ItemsSource)
        {
            Node node = new Node(keyValue.Key);
            _nodeList.Add(node);
        }
        MultiSelectCombo.ItemsSource = _nodeList;
    }

    private void SetText()
    {
        if (this.SelectedItems != null)
        {
            StringBuilder displayText = new StringBuilder();
            foreach (Node s in _nodeList)
            {
                if (s.IsSelected == true && s.Title == "All")
                {
                    displayText = new StringBuilder();
                    displayText.Append("All");
                    break;
                }
                else if (s.IsSelected == true && s.Title != "All")
                {
                    displayText.Append(s.Title);
                    displayText.Append(',');
                }
            }
            this.Text = displayText.ToString().TrimEnd(new char[] { ',' }); 
        }           
        // set DefaultText if nothing else selected
        if (string.IsNullOrEmpty(this.Text))
        {
            this.Text = this.DefaultText;
        }
    }   
    #endregion
}
