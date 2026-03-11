using System;
using System.Collections.Generic;

namespace NewLibre.Models;
public class Node
{
    public string Name { get; set; }
    public bool IsFolder { get; set; }
    public List<Node> Children { get; set; }
}

