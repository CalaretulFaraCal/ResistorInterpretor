using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Logic
{
    public class ColorConverterLogic(ComboBox bandSelector, IComboBoxManager[] colorManagers, Label resultLabel) : IColorConverterLogic
    {
        public void Convert()
        {
            int bandCount = bandSelector.SelectedIndex + 3;

            var propertyTypes = new[]
            {
                "comboBox1", "comboBox2", "comboBox3",
                "comboBox4", "comboBox5", "comboBox6"
            };

            var selectedColors = new string[colorManagers.Length];
            for (int i = 0; i < colorManagers.Length; i++)
            {
                selectedColors[i] = colorManagers[i].GetSelectedColor(propertyTypes[i], "Black");
            }

            var colors = selectedColors
                .Select(name => ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == name))
                .ToArray();

            int digits = 0;
            double multiplier = 1;
            double? tolerance = null;
            int? tempCoeff = null;

            if (bandCount == 3 || bandCount == 4)
            {
                digits = (colors[0]?.Digit ?? 0) * 10 + (colors[1]?.Digit ?? 0);
                multiplier = Math.Pow(10, colors[2]?.MultiplierExponent ?? 0);
                if (bandCount == 4)
                    tolerance = colors[3]?.Tolerance;
            }
            else if (bandCount == 5 || bandCount == 6)
            {
                digits = (colors[0]?.Digit ?? 0) * 100 +
                         (colors[1]?.Digit ?? 0) * 10 +
                         (colors[2]?.Digit ?? 0);
                multiplier = Math.Pow(10, colors[3]?.MultiplierExponent ?? 0);
                tolerance = colors[4]?.Tolerance;
                if (bandCount == 6)
                    tempCoeff = colors[5]?.TemperatureCoefficient;
            }

            double value = digits * multiplier;
            string result = FormatResult(value, tolerance, tempCoeff);
            resultLabel.Text = "Resistance: " + result;
        }

        private string FormatWithSuffix(double value)
        {
            if (value >= 1_000_000)
                return $"{value / 1_000_000:0.##}M";
            if (value >= 1_000)
                return $"{value / 1_000:0.##}k";
            return value.ToString();
        }

        private string FormatResult(double value, double? tolerance, int? tempCoeff)
        {
            var result = FormatWithSuffix(value);
            if (tolerance.HasValue)
                result += $" ±{tolerance.Value}%";
            if (tempCoeff.HasValue)
                result += $" {tempCoeff.Value}ppm/K";
            return result;
        }

        public void UpdateBandComboBoxVisibility(int bandCount, int previousBandCount, IComboBoxManager[] comboBox)
        {
            comboBox[2].UpdateComboBoxVisibility(bandCount, previousBandCount, "comboBox3");
            comboBox[4].UpdateComboBoxVisibility(bandCount, previousBandCount, "comboBox5");
            comboBox[5].UpdateComboBoxVisibility(bandCount, previousBandCount, "comboBox6");
        }
    }
}
