using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services;

public class ComboBoxManager(ComboBox comboBox) : IComboBoxManager
{
    public ComboBox ComboBox => comboBox;
    public string GetSelectedColor(string propertyType, string defaultColor)
    {
        if (!comboBox.Visible || comboBox.SelectedIndex < 0)
            return defaultColor;

        var selectedText = comboBox.SelectedItem + "";

        var startIndex = selectedText.IndexOf('(');
        var endIndex = selectedText.IndexOf(')');
        if (startIndex >= 0 && endIndex > startIndex)
            return selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);

        return selectedText;
    }
    public void PopulateComboBox(string propertyType)
    {
        var previousSelection = comboBox.SelectedItem?.ToString();
        comboBox.Items.Clear();

        void AddColors(Func<ResistorColorInfo, bool> predicate, Func<ResistorColorInfo, string> formatter)
        {
            foreach (var c in ResistorColorInfo.AllColors.Where(predicate))
                comboBox.Items.Add(formatter(c));
        }

        switch (propertyType)
        {
            case "filterCTV":
                comboBox.Items.Clear();
                comboBox.Items.AddRange(new object[] { "None", "3 bands", "4 bands", "5 bands", "6 bands", "Black", "Brown", "Red", "Orange", "Yellow", "Green", "Blue", "Violet", "Gray", "White", "Gold", "Silver" });
                comboBox.SelectedIndex = 0;
                break;

            case "sortCTV":
                comboBox.Items.Clear();
                comboBox.Items.AddRange(new object[] { "All" ,"Band Count", "Color 1", "Color 2", "Color 3", "Color 4", "Color 5", "Color 6", "Time" });
                comboBox.SelectedIndex = 0; 
                break;

            case "filterVTC":
                comboBox.Items.Clear();
                comboBox.Items.AddRange(new object[] { "None", "Ohm", "kOhm", "MOhm", "GOhm", "3 bands", "4 bands", "5 bands", "6 bands" });
                comboBox.SelectedIndex = 0; 
                break;

            case "sortVTC":
                comboBox.Items.Clear();
                comboBox.Items.AddRange(new object[] { "All", "Value", "Bands", "Tolerance", "TempCoeff" });
                comboBox.SelectedIndex = 0;
                break;

            case "units":
                comboBox.Items.Clear();
                comboBox.Items.AddRange(new object[] { "Ohm", "kOhm", "MOhm", "GOhm" });
                comboBox.SelectedIndex = 0;
                break;

            case "bands":
                comboBox.Items.AddRange(new object[] { "3 bands", "4 bands", "5 bands", "6 bands" });
                break;

            case "tolerance":
                AddColors(c => c.Tolerance.HasValue, c => $"±{c.Tolerance}% ({c.Name})");
                break;

            case "temperatureCoefficient":
                AddColors(c => c.TemperatureCoefficient.HasValue, c => $"{c.TemperatureCoefficient}ppm/K ({c.Name})");
                break;

            case "comboBox1":
            case "comboBox2":
            case "comboBox3":
                AddColors(c => c.Digit.HasValue, c => c.Name + "");
                break;

            case "comboBox4":
                AddColors(c => c.MultiplierExponent.HasValue, c => c.Name + "");
                break;

            case "comboBox5":
                AddColors(c => c.Tolerance.HasValue, c => c.Name + "");
                break;

            case "comboBox6":
                AddColors(c => c.TemperatureCoefficient.HasValue, c => c.Name + "");
                break;
        }

        RestoreComboBoxSelection(previousSelection ?? "");
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
            "comboBox3" => bandCount >= 5,   
            "comboBox5" => bandCount >= 4,   
            "comboBox6" => bandCount == 6,
            _ => comboBox.Visible
        };

        if (comboBox.Visible && bandCount != previousBandCount)
        {
            string? previousSelection = comboBox.SelectedItem?.ToString();
            PopulateComboBox(propertyType);
            RestoreComboBoxSelection(previousSelection + "");
        }
    }

    public void SetSelectedItem(string text)
    {
        for (int i = 0; i < comboBox.Items.Count; i++)
        {
            if (comboBox.Items[i].ToString() == text)
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
    }

    public int GetItemCount()
    {
        return comboBox.Items.Count;
    }

    public object GetItemAt(int index)
    {
        return comboBox.Items[index];
    }

    public void SetSelectedIndex(int index)
    {
        comboBox.SelectedIndex = index;
    }
}