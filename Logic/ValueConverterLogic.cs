using ResistorInterpretor.Contracts;
using System.Globalization;

namespace ResistorInterpretor.Logic;

public class ValueConverterLogic(IMainFormUI ui, IListManager listManager, IComboBoxManager comboBox1, IComboBoxManager comboBox2) : IValueConverterLogic
{
    public int previousBandCount { get; set; } = 3;

    public event EventHandler<ValueConversionEventArgs> ConversionCompleted;

    public void Convert()
    {
        if (!double.TryParse(ui.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double value) || value <= 0)
        {
            UI.ShowMessage("Enter a valid number.");
            return;
        }

        switch (ui.Suffix)
        {
            case "kOhm": value *= 1_000; break;
            case "MOhm": value *= 1_000_000; break;
            case "GOhm": value *= 1_000_000_000; break;
        }

        int bandCount = previousBandCount;
        int significantDigitCount = bandCount >= 5 ? 3 : 2;

        string significantDigits = "";
        int multiplierIndex = -1;

        for (int exponent = -2; exponent <= 9; exponent++)
        {
            double testValue = value / Math.Pow(10, exponent);
            int maxValue = (int)Math.Pow(10, significantDigitCount);
            int rounded = (int)Math.Round(testValue);

            if (rounded >= 0 && rounded < maxValue)
            {
                significantDigits = rounded.ToString().PadLeft(significantDigitCount, '0');
                multiplierIndex = exponent switch
                {
                    -2 => 11, // Silver
                    -1 => 10, // Gold
                    >= 0 and <= 9 => exponent,
                    _ => -1
                };
                break;
            }
        }

        if (multiplierIndex == -1 || string.IsNullOrEmpty(significantDigits))
        {
            UI.ShowMessage("Value out of range.");
            return;
        }

        // Collect data for history
        string? toleranceColor = comboBox1.GetSelectedColor("tolerance", null);
        string? tempCoeffColor = comboBox2.GetSelectedColor("temperatureCoefficient", null);

        // Trigger the event with all necessary data
        ConversionCompleted?.Invoke(this, new ValueConversionEventArgs(
            double.Parse(ui.Value, CultureInfo.InvariantCulture),
            ui.Suffix,
            previousBandCount,
            toleranceColor,
            tempCoeffColor));

        GenerateBands(significantDigits, multiplierIndex, bandCount);

    }

    private void GenerateBands(string significantDigits, int multiplierIndex, int bandCount)
    {
        listManager.Clear();

        // Add significant digit bands
        foreach (var c in significantDigits)
        {
            var digit = c - '0';
            var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(ci => ci.Digit == digit);
            if (colorInfo != null)
                listManager.AddBand(colorInfo.Color, colorInfo.Name);
        }

        // Add multiplier band
        if (multiplierIndex >= 0 && multiplierIndex < 12)
        {
            var multiplierColor = ResistorColorInfo.AllColors[multiplierIndex];
            listManager.AddBand(multiplierColor.Color, multiplierColor.Name);
        }

        // Add tolerance band 
        if (bandCount > 3)
        {
            var colorName = comboBox1.GetSelectedColor("tolerance", "Brown");
            var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
            if (colorInfo?.Tolerance.HasValue == true)
                listManager.AddBand(colorInfo.Color, $"{colorName} (±{colorInfo.Tolerance}%)");
        }

        // Add temperature coefficient band
        if (bandCount == 6)
        {
            var colorName = comboBox2.GetSelectedColor("temperatureCoefficient", "Brown");
            var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
            if (colorInfo?.TemperatureCoefficient.HasValue == true)
                listManager.AddBand(colorInfo.Color, $"{colorName} ({colorInfo.TemperatureCoefficient}ppm/K)");
        }
    }

    public void UpdateToleranceVisibility(int bandCount)
    {
        comboBox1.UpdateComboBoxVisibility(bandCount, previousBandCount, "tolerance");

    }

    public void UpdateTemperatureCoefficientVisibility(int bandCount)
    {
        comboBox2.UpdateComboBoxVisibility(bandCount, previousBandCount, "temperatureCoefficient");
    }
    public class ValueConversionEventArgs : EventArgs
    {
        public double Value { get; }
        public string Unit { get; }
        public int BandCount { get; }
        public string? ToleranceColor { get; }
        public string? TempCoeffColor { get; }

        public ValueConversionEventArgs(
            double value,
            string unit,
            int bandCount,
            string? toleranceColor,
            string? tempCoeffColor)
        {
            Value = value;
            Unit = unit;
            BandCount = bandCount;
            ToleranceColor = toleranceColor;
            TempCoeffColor = tempCoeffColor;
        }
    }
}