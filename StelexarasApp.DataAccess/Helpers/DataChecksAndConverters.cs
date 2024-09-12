using StelexarasApp.DataAccess.Models.Domi;
using System.Globalization;

namespace StelexarasApp.DataAccess.Helpers
{    
    public static class DataChecksAndConverters // : IValueConverter
    {
        public static bool IsValidFullNameInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var parts = input.Trim().Split(' ');
            return parts.Length >= 2;
        }

        public static object ConvertEidosXwrouToString(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EidosXwrou xwros)
            {
                switch (xwros)
                {
                    case EidosXwrou.Skini:
                        return "Σκηνή";
                    case EidosXwrou.Koinotita:
                        return "Κοινότητα";
                    case EidosXwrou.Tomeas:
                        return "Τομεας";
                    default:
                        return "Unknown Title";
                }
            }
            return "Default Title";
        }

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
