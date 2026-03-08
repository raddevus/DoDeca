using System.Collections.ObjectModel;

namespace Models.NewLibre;

public class Folder
{
  public ObservableCollection<Folder>? SubItems { get; }
  public string Title { get; }

  public Folder(string title)
  {
      Title = title;
  }

  public Folder(string title, ObservableCollection<Folder> subItem)
  {
      Title = title;
      SubItems = subItem;
  }

  public override string ToString(){
     return Title;
  }
}
