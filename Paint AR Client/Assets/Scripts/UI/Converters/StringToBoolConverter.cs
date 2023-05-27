using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;

namespace ArPaint.UI.Converters
{
    public class StringToBoolConverter : PropertyValueConverter<string, bool>
    {
        public override bool Convert(string value)
            => string.IsNullOrWhiteSpace(value);

        public override string ConvertBack(bool value)
            => value.ToString();
    }
}