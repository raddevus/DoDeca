using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Models.NewLibre;
public enum StorageType{ File, Directory};
public class Node: INotifyPropertyChanged{
    public string Name { get; set; }
    public StorageType StoreType { get; set; }
    public string Path {get;set;}
    private string iconSource;
    public string IconSource {
        get => iconSource;
        set
        {
            if (iconSource != value)
            {
                iconSource = value;
                OnPropertyChanged(nameof(IconSource));
            }
        }
    }
     public ObservableCollection<Node> Children { get; set; }
        = new ObservableCollection<Node>();
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public override string ToString(){
       return Name;
    }
}

