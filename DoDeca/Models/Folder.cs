using System.Collections.ObjectModel;

namespace Models.NewLibre;

public class Folder
{
  public ObservableCollection<Folder>? SubItems { get; } = new();
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
  public override bool Equals(object obj){
     // Check if the compared object is null and if it's of the same type
     if (obj == null || GetType() != obj.GetType())
         return false;

     Folder other = (Folder)obj;

     return Title == other.Title;
 }
}
