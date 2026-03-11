using System;
using System.Collections.Generic;

namespace Models.NewLibre;
public class Node{
    public string Name { get; set; }
    public bool IsFolder { get; set; }
    public string Path {get;set;}
    public List<Node> Children { get; set; } = new();
    public override string ToString(){
       return Name;
    }
}

