using ResistorInterpretor.Contracts;
using System.Xml.Serialization;

namespace ResistorInterpretor.Services;

public class ComboBoxManager(ComboBox comboBox) : IComboBoxManager
{
    public string GetSelectedColor(string propertyType, string defaultColor)
    {
        if (!comboBox.Visible || comboBox.SelectedIndex < 0)
            return defaultColor;

        var selectedText = comboBox.SelectedItem + "";
        var startIndex = selectedText.IndexOf('(');
        var endIndex = selectedText.IndexOf(')');
        if (startIndex >= 0 && endIndex > startIndex)
            return selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);
        return defaultColor;
    }

    public void PopulateUnitComboBox()
    {
        comboBox.Items.Clear();
        comboBox.Items.AddRange(new object[] { "Ohm", "kOhm", "MOhm" });
        comboBox.SelectedIndex = 0;
    }

    public void PopulateComboBox(string propertyType)
    {
        var previousSelection = comboBox.SelectedItem?.ToString();
        comboBox.Items.Clear();

        if (propertyType == "bands")
            comboBox.Items.AddRange(new object[] { "3 bands", "4 bands", "5 bands", "6 bands" });

        else if (propertyType == "tolerance")
        {
            foreach (var c in ResistorColorInfo.AllColors)
                if (c.Tolerance.HasValue)
                    comboBox.Items.Add($"±{c.Tolerance}% ({c.Name})");
        }

        else if (propertyType == "temperatureCoefficient")
        {
            foreach (var c in ResistorColorInfo.AllColors)
                if (c.TemperatureCoefficient.HasValue)
                    comboBox.Items.Add($"{c.TemperatureCoefficient}ppm/K ({c.Name})");
        }

        RestoreComboBoxSelection(previousSelection);
    }

    public void RestoreComboBoxSelection(string previousSelection)
    {
        if (previousSelection != null)
        {
            var previousIndex = comboBox.Items.IndexOf(previousSelection);
            comboBox.SelectedIndex = previousIndex >= 0 ? previousIndex : 0;
        }
        else if (comboBox.Items.Count > 0)
            comboBox.SelectedIndex = 0;
    }

    public void UpdateComboBoxVisibility(int bandCount, int previousBandCount, string propertyType)
    {
        comboBox.Visible = propertyType switch
        {
            "tolerance" => bandCount > 3,
            "temperatureCoefficient" => bandCount == 6,
            _ => comboBox.Visible
        };

        if (comboBox.Visible && bandCount != previousBandCount)
        {
            string? previousSelection = comboBox.SelectedItem?.ToString();
            PopulateComboBox(propertyType);
            RestoreComboBoxSelection(previousSelection);
        }
    }

}