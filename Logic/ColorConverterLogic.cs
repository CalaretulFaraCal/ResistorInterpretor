using ResistorInterpretor.Contracts;
namespace ResistorInterpretor.Logic
{
    public class ColorConverterLogic(ComboBox bandSelector, IComboBoxManager[] colorManagers, Label resultLabel, ILabelManager[] labelManager) : IColorConverterLogic
    {
        public event EventHandler<ColorConversionEventArgs> ConversionCompleted;
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
                selectedColors[i] = colorManagers[i].GetSelectedColor(propertyTypes[i], "Black");

            var colors = selectedColors
                .Select(name => ResistorColorInfo.AllColors.First(c => c.Name == name))
                .ToArray();

            int digits = 0;
            double multiplier = 1;
            double? tolerance = null;
            int? tempCoeff = null;

            if (bandCount == 3 || bandCount == 4)
            {
                digits = (colors[0]?.Digit ?? 0) * 10 +
                         (colors[1]?.Digit ?? 0);
            }
            else if (bandCount == 5 || bandCount == 6)
            {
                digits = (colors[0]?.Digit ?? 0) * 100 +
                         (colors[1]?.Digit ?? 0) * 10 +
                         (colors[2]?.Digit ?? 0);
                tolerance = colors[4]?.Tolerance;
            }
            multiplier = Math.Pow(10, colors[3]?.MultiplierExponent ?? 0);

            if (bandCount >= 4)
                tolerance = colors[4]?.Tolerance;
            if (bandCount == 6)
                tempCoeff = colors[5]?.TemperatureCoefficient;

            double value = digits * multiplier;
            string result = FormatResult(value, tolerance, tempCoeff);
            resultLabel.Text = "Resistance: " + result;


        }

        private string FormatWithSuffix(double value)
        {
            if (value >= 1_000_000_000)
                return $"{value / 1_000_000_000:0.##}GOhm";
            if (value >= 1_000_000)
                return $"{value / 1_000_000:0.##}MOhm";
            if (value >= 1_000)
                return $"{value / 1_000:0.##}kOhm";
            return $"{value:0.##} Ohm";
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

            labelManager[2].UpdateLabelVisibility("comboBox3", bandCount);
            labelManager[4].UpdateLabelVisibility("comboBox5", bandCount);
            labelManager[5].UpdateLabelVisibility("comboBox6", bandCount);
        }

        public class ColorConversionEventArgs : EventArgs
        {
            public int BandCount { get; }
            public List<string> ColorBandNames { get; }

            public ColorConversionEventArgs(int bandCount, List<string> colorBandNames)
            {
                BandCount = bandCount;
                ColorBandNames = colorBandNames;
            }
        }
    }
}
