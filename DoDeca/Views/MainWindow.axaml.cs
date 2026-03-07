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
      OpenPane.OpenPaneLength += 10;
   }

}
