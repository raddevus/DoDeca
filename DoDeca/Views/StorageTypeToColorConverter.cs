using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia.Media;
using Models.NewLibre;
using Avalonia;
using Avalonia.Styling;   // For ThemeVariant
using DoDeca.Views;

namespace DoDeca.Views;
public record FileSystemColors(IBrush Folder, IBrush File);
public class StorageTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
     if (value is StorageType storageType)
     {
         return storageType == StorageType.Directory ? MainWindow.FileSysColors.Folder : MainWindow.FileSysColors.File;
      }
        return Brushes.Black; // Fallback color
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

