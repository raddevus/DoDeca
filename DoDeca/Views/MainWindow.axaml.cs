using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;  // Adds items necessary for event handlers
using Avalonia.Media;
using Avalonia.VisualTree;
using Avalonia.Styling;
using System;
using System.Text.Json;
using System.Collections.Generic;
using Models.NewLibre;
using System.Collections.ObjectModel;
using DoDeca.ViewModels;
using System.IO;
using System.Linq;

namespace DoDeca.Views;

public partial class MainWindow : Window
{
   string rootPath = string.Empty;
   int nodeDepth = 0;
   public static FileSystemColors FileSysColors;
   public MainWindow()
    {
        InitializeComponent();
        FileTree.AddHandler(InputElement.PointerPressedEvent,
             OnTreePointerPressed, RoutingStrategies.Tunnel);
        FileTree.AddHandler(
    InputElement.PointerReleasedEvent,
    (_, e) => Console.WriteLine($"PointerReleased handled={e.Handled} source={e.Source}"),
    RoutingStrategies.Bubble); 
      InitThemeChangeHandler();

    }

    protected override void OnOpened(EventArgs e){
       base.OnOpened(e);
       CheckThemeVariant();
       Finder f = new();
       var specFolders = f.GetSpecialFolders();
Console.WriteLine($"{specFolders}");
       var fd = JsonSerializer.Deserialize<List<FolderData>>(specFolders);
       Console.WriteLine($"fd {fd.GetType()}");
       foreach (var fx in fd){
          Console.WriteLine($"foldername: {fx.folderName}");
          QuickLinksLB.Items.Add(fx);
       }
          NavPathTB.Text = fd.FirstOrDefault(a => a.folderName == "UserProfile")?.folderPath ?? string.Empty;
      if (NavPathTB.Text != string.Empty){
         NavigateToPath();
      }
    }

   private void CheckThemeVariant(){
       Console.WriteLine($"theme: {ActualThemeVariant}"); 
      if (ActualThemeVariant == ThemeVariant.Dark)
      {
         FileSysColors = new(Brushes.Orange, Brushes.Yellow);
      }
      else
      {
         FileSysColors = new (Brushes.Blue, Brushes.Green);
      }
    }

   private void InitThemeChangeHandler(){
       if (Application.Current == null){return;}
          Application.Current.ActualThemeVariantChanged += (s, e) =>
         {
                Console.WriteLine($"ThemeVariant: {Application.Current.ActualThemeVariant}"); 
                CheckThemeVariant();
             if (Application.Current.ActualThemeVariant == ThemeVariant.Dark)
             { 
               FileSysColors = new(Brushes.Orange, Brushes.Yellow);
             }
             else
             {
               FileSysColors = new (Brushes.Blue, Brushes.Green);
             }
             NavigateToPath();
         };
   }
   private async void GoUpOneFolder(object? sender, RoutedEventArgs e){
         string navPath = NavPathTB.Text; 
         var splitPaths = navPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
         if (splitPaths != null && splitPaths.Length > 0){
         Console.WriteLine($"splitPaths {splitPaths?.Length} : {splitPaths?[0] ?? ""}");
          
         Console.WriteLine(string.Join(Path.DirectorySeparatorChar, splitPaths));
          var toBeRemoved = splitPaths[splitPaths.Length-1];
          var lastIdx = navPath.LastIndexOf(toBeRemoved);
          var finalPath = lastIdx >= 1 ? navPath.Remove(lastIdx,toBeRemoved.Length) : navPath;
          if (splitPaths?.Length == 1 && finalPath.Contains(Path.PathSeparator)){
             Console.WriteLine($"finalPath.Length: {finalPath.Length}  finalPath : {finalPath}");
          }
          if (finalPath.Length > 1){
          finalPath = finalPath.TrimEnd(Path.DirectorySeparatorChar);
          }
          if (finalPath.Length == 2 && finalPath.Contains(Path.PathSeparator)){ 
             finalPath = Path.EndsInDirectorySeparator(finalPath) ? finalPath : finalPath + Path.DirectorySeparatorChar;
          }
          NavPathTB.Text = finalPath;
          NavigateToPath();

         } 
   }

    private void OnTreePointerPressed(object? sender, PointerPressedEventArgs e)
   {
      e.Handled = false;
       // Find the TreeViewItem that was clicked
       var treeViewItem = (e.Source as Visual)?.FindAncestorOfType<TreeViewItem>();
       if (treeViewItem == null)
           return;

       var data = treeViewItem.DataContext;
       int depth = GetNodeDepth(treeViewItem);
       nodeDepth = depth;
       if (!treeViewItem.IsExpanded){
         treeViewItem.IsExpanded = true ;
       }

       Console.WriteLine($"Clicked node: {data}, depth: {depth}");
   }

private int GetNodeDepth(TreeViewItem item)
   {
       int depth = 0;
       Visual? parent = item;

       while (true)
       {
           parent = parent.FindAncestorOfType<TreeViewItem>();
           if (parent == null)
               break;

           depth++;
       }

       return depth;
   }
   private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
   {
       // Check if the Enter key was pressed
       if (e.Key == Key.Enter)
       {
           // Call the function you want to run
           NavigateToPath();
           // Optionally, you may want to suppress the key press event
           // e.Handled = true;
       }
   }
   string currentPath = string.Empty;

    private void NavigateToPath(){
       rootPath = currentPath = NavPathTB.Text;
       if (!Directory.Exists(currentPath)){
          return;
       }
      TraversePath(currentPath);
    }

   private void TraversePath(string path){

       var vm = (MainWindowViewModel)DataContext;
       vm.AllNodes.Clear();
       Console.WriteLine(path);
       Finder f = new();
       var allDirs = f.GetFileInfo(path);
       foreach (Tuple<FileAttributes, string>fn in allDirs){
          vm.AllNodes.Add(new Node(){
                Name = fn.Item2.ToString(),
                StoreType = fn.Item1 == FileAttributes.Directory ? StorageType.Directory: StorageType.File,
                IconSource = (fn.Item1 == FileAttributes.Directory) ? "avares://DoDeca/Assets/folder.png" : "avares://DoDeca/Assets/file.png", 
                Path = path});
       }
   }

    private async void TviClick(object? sender, SelectionChangedEventArgs e){

       var targetNode = (sender as TreeView)?.SelectedItem as Node;
       // If it's not a folder (it's a file) nothing more to do
       if (targetNode?.StoreType == StorageType.File){return;}
       // Check to see if the targetNode already has Children
       // because if it does then it has already been traversed
       // and we need to remove the Children so they aren't
       // added twice
       if (targetNode?.Children?.Count > 0){ targetNode.Children.Clear();}
       var parentFolder = targetNode.Path;
       Console.WriteLine($"{targetNode}");
       var targetPath = string.Empty;
       
       targetPath = Path.Combine(parentFolder,targetNode.ToString());
       currentPath = targetPath;
       Console.WriteLine($"targetPath: {targetPath}");
       Finder f = new();
       NavPathTB.Text = currentPath = targetPath;
       try{
          var allDirs = f.GetFileInfo(targetPath);
          Console.WriteLine($"allDirs.Count : {allDirs.Count}");
          foreach (Tuple<FileAttributes, string> fn in allDirs){
            Console.WriteLine(fn);
            var node = new Node(){
               StoreType = fn.Item1 == FileAttributes.Directory ? StorageType.Directory : StorageType.File,
               Name = fn.Item2,
                IconSource = (fn.Item1 == FileAttributes.Directory) ? "avares://DoDeca/Assets/folder.png" : "avares://DoDeca/Assets/file.png", 
               Path = targetPath};
            Console.WriteLine(node.IconSource);
            if (!targetNode.Children.Contains(node)){
               Console.WriteLine($"Has {node.Children?.Count}. It doesn't contain folder!?");
               targetNode.Children.Add(node);
            }
          }
       }
       catch (Exception ex){
          Console.WriteLine($"Error: : {ex.Message}");
       }
    }

    private async void QuickLinkChanged(object? sender, RoutedEventArgs e){
      string path = ((sender as ListBox)?.SelectedItem as FolderData)?.folderPath ?? string.Empty;
      if (path == string.Empty){return;}
      rootPath = NavPathTB.Text = currentPath = path;
      TraversePath(currentPath);
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
