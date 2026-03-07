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
   private async void MakeBigger(object? sender, RoutedEventArgs e){
      if (isCtrlDown){ OpenPane.OpenPaneLength -= 10; return;}
      OpenPane.OpenPaneLength += 10;
   }
   bool isCtrlDown = false;
   private void Window_KeyDown(object? sender, KeyEventArgs e)
   {
      Console.WriteLine($"e.Key: {e.Key}");
       if (e.Key == Key.LeftCtrl)
       {
          isCtrlDown = true;
          ChangeSize.Content = "Make Smaller";
           Console.WriteLine("Control key pressed");
       }
   }

   private void Window_KeyUp(object? sender, KeyEventArgs e){
      if (e.Key == Key.LeftCtrl){ 
         isCtrlDown = false;
         ChangeSize.Content = "Make Bigger";
      }
   }
}
