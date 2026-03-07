using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;  // Adds items necessary for event handlers
using System;

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
