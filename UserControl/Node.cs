using CommunityToolkit.Mvvm.ComponentModel;

namespace PersonsDemo;

public partial class Node : ObservableObject
{
    #region Properties
    [ObservableProperty]
    private string _title;
    [ObservableProperty]
    private bool _isSelected;
    #endregion

    #region Constructor
    public Node(string title)
    {
        Title = title;
    }
    #endregion
}
