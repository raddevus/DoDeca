
using System.Collections.ObjectModel;
using Models.NewLibre;

namespace DoDeca.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Node> AllNodes{get;set;} = new();
}
