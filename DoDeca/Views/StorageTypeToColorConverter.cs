using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia.Media;
using Models.NewLibre;
namespace DoDeca.Views;
public class StorageTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is StorageType storageType)
        {
            return storageType == StorageType.Directory ? Brushes.Blue : Brushes.Green;
        }
        return Brushes.Black; // Fallback color
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

