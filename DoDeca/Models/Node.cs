using System;
using System.Collections.ObjectModel;

namespace Models.NewLibre;
public enum StorageType{ File, Directory};
public class Node{
    public string Name { get; set; }
    public StorageType StoreType { get; set; }
    public string Path {get;set;}
    public string IconSource { get; set; } // Icon source for the tree view node
     public ObservableCollection<Node> Children { get; set; }
        = new ObservableCollection<Node>();
    public override string ToString(){
       return Name;
    }
}

