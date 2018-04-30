using System;
using Xamarin.Forms;
using System.Globalization;
namespace PostListDetailsXamarin
{
    /// <summary>
    ///  Converter that retturn the selected item from the class SelectedItemChangedEventArgs which the ItemSelected event parameters
    /// </summary>  
    public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectedItemChangedEventArgs;
            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
