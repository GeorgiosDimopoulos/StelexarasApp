using StelexarasApp.DataAccess.Models.Domi;
using System.Globalization;

namespace StelexarasApp.DataAccess.Helpers
{    
    public static class EnumToTitleConverter // : IValueConverter
    {
        public static object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
