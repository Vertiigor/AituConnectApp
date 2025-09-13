using System.Globalization;

namespace AituConnectApp
{
    public class RepliesToHeightConverter : IValueConverter
    {
        // Adjust this constant to match approx. row height of a single reply
        private const double RowHeight = 80; // px

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                if (count <= 0) return 0;

                const double rowHeight = 80;
                const double maxHeight = 400;

                return Math.Min(count * rowHeight, maxHeight);
            }
            return 0;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
