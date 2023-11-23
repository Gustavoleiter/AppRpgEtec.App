using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AppRpgEtec.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imaageAsBytes = (byte[])value;
                retSource = ImageSource.FromStream(() => new MemoryStream(imaageAsBytes));
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targeType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
