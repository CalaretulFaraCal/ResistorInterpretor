using ResistorInterpretor.Contracts;

namespace ResistorInterpretor;

public class ConverterLogic(IMainFormUI ui, IListManager listManager, IComboBoxManager comboBox1, IComboBoxManager comboBox2)
{
    public int PreviousBandCount { get; set; } = 3;

    public void Convert()
    {
        if (!double.TryParse(ui.Value, out var value) || value <= 0)
        {
            UI.ShowMessage("Enter a valid number.");
            return;
        }

        switch (ui.Suffix)
        {
            case "kOhm": value *= 1_000; break;
            case "MOhm": value *= 1_000_000; break;
        }

        var bandCount = PreviousBandCount;
        var significantDigitCount = bandCount >= 5 ? 3 : 2;

        var significantDigits = "";
        var multiplierIndex = -1;

        for (var exponent = -2; exponent <= 9; exponent++)
        {
            var testValue = value / Math.Pow(10, exponent);
            var maxValue = (int)Math.Pow(10, significantDigitCount);
            var rounded = (int)Math.Round(testValue);

            if (rounded >= 0 && rounded < maxValue)
            {
                significantDigits = rounded.ToString().PadLeft(significantDigitCount, '0');
                multiplierIndex = exponent switch
                {
                    -2 => 11, // Silver
                    -1 => 10, // Gold
                    >= 0 and <= 9 => exponent,
                    _ => -1,
                };
                break;
            }
        }

        if (multiplierIndex == -1 || string.IsNullOrEmpty(significantDigits))
        {
            UI.ShowMessage("Value out of range.");
            return;
        }

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
            var colorName = comboBox1.GetSelectedColor("tolerance", "Gold");
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
}