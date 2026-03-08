using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;  // Adds items necessary for event handlers
using System;
using System.Text.Json;
using System.Collections.Generic;
using Models.NewLibre;

namespace DoDeca.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnOpened(EventArgs e){
       base.OnOpened(e);
       QuickLinksLB.Items.Add("test");
       Finder f = new();
       var specFolders = f.GetSpecialFolders();
Console.WriteLine($"{specFolders}");
       var fd = JsonSerializer.Deserialize<List<FolderData>>(specFolders);
       Console.WriteLine($"fd {fd.GetType()}");
       foreach (var fx in fd){
          Console.WriteLine($"foldername: {fx.folderName}");
          QuickLinksLB.Items.Add(fx.folderName);
       }
    }
    
/*   private async void MakeBigger(object? sender, RoutedEventArgs e){
      if (isCtrlDown){ OpenNavPane.OpenNavPaneLength -= 10; return;}
      OpenNavPane.OpenNavPaneLength += 10;
   } */
   bool isCtrlDown = false;
   private void Window_KeyDown(object? sender, KeyEventArgs e)
   {
/*      Console.WriteLine($"e.Key: {e.Key}");
       if (e.Key == Key.LeftCtrl)
       {
          isCtrlDown = true;
          ChangeSize.Content = "Make Smaller";
           Console.WriteLine("Control key pressed");
       }*/
   }
   private bool _isDragging;

private void OnSplitterDragStarted(object? sender, VectorEventArgs e)
{
    _isDragging = true;
    Console.WriteLine("Pressed...");
}

private void OnSplitterDragCompleted(object? sender, VectorEventArgs e)
{
    _isDragging = false;
    Console.WriteLine($"Released...{e.Vector.X}");

    // 🔥 This is your "DragCompleted" equivalent
    OnSplitterDragCompleted();
}


private void OnSplitterDragCompleted()
{
    // Put your "drag finished" logic here
    Console.WriteLine("Splitter drag completed.");
//    NavPane.Width = 150;
}

   private void OnSplitterDragDelta(object? sender, VectorEventArgs e)
   {
       var newWidth = NavPane.Width + e.Vector.X;

       if (newWidth <= NavPane.MinWidth)
           newWidth = NavPane.MinWidth;

       if (newWidth >= NavPane.MaxWidth)
           newWidth = NavPane.MaxWidth;

    //   NavPane.Width = 50; //newWidth;
   }
   private void Window_KeyUp(object? sender, KeyEventArgs e){
/*      if (e.Key == Key.LeftCtrl){ 
         isCtrlDown = false;
         ChangeSize.Content = "Make Bigger";
      } */
   } 
}
// NOTE: When deserializing from JSON & using a record
// the property names much match case in the JSON
public class FolderData{
   public string folderName{get;set;}
   public string folderPath{get;set;}
   public FolderData(){} 
   public FolderData(string name, string path){
      folderName = name;
      folderPath = path;
   }
   public override string ToString(){
      return folderName;
   }
}
