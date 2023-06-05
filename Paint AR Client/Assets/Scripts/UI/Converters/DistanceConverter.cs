using UnityEngine;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;

namespace ArPaint.UI.Converters
{
    public class DistanceConverter : PropertyValueConverter<float, string>
    {
        public override string Convert(float value)
            => $"{Mathf.FloorToInt(value * 100f)} cm";

        public override float ConvertBack(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}