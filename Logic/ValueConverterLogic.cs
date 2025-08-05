using ResistorInterpretor.Contracts;
using System.Globalization;

namespace ResistorInterpretor.Logic;

public class ValueConverterLogic(IMainFormUI ui, IListManager listManager, IComboBoxManager comboBox1, IComboBoxManager comboBox2, IGenerateBandsManager generateBandsManager) : IValueConverterLogic
{
    public int previousBandCount { get; set; } = 3;
    private bool suppressHistory = false;
    private readonly IGenerateBandsManager _generateBandsManager = generateBandsManager;
    public event EventHandler<ValueConversionEventArgs> HistoryEntry;

    public void Convert(bool suppressHistory = false)
    {
        var valueText = ui.Value.Replace(',', '.');
        if (!double.TryParse(valueText, NumberStyles.Any, CultureInfo.InvariantCulture, out double value) || value <= 0)
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
        if (!suppressHistory)
        {
            HistoryEntry?.Invoke(this, new ValueConversionEventArgs(
                double.Parse(ui.Value, CultureInfo.InvariantCulture),
                ui.Suffix,
                previousBandCount,
                toleranceColor,
                tempCoeffColor));
        }

        GenerateBands(significantDigits, multiplierIndex, bandCount);
    }

    private void GenerateBands(string significantDigits, int multiplierIndex, int bandCount)
    {
        listManager.Clear();
        string? toleranceColor = comboBox1.GetSelectedColor("tolerance", null);
        string? tempCoeffColor = comboBox2.GetSelectedColor("temperatureCoefficient", null);

        var bands = _generateBandsManager.FromValue(significantDigits, multiplierIndex, bandCount, toleranceColor, tempCoeffColor);
        foreach (var (color, label) in bands)
            listManager.AddBand(color, label);
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