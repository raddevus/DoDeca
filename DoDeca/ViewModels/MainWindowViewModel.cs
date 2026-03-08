
using System.Collections.ObjectModel;
using Models.NewLibre;

namespace DoDeca.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    public ObservableCollection<Folder> Folders { get;set; } = new();
}
