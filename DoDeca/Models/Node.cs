using System;
using System.Collections.ObjectModel;

namespace Models.NewLibre;
public class Node{
    public string Name { get; set; }
    public bool IsFolder { get; set; }
    public string Path {get;set;}
     public ObservableCollection<Node> Children { get; set; }
        = new ObservableCollection<Node>();
    public override string ToString(){
       return Name;
    }
}

