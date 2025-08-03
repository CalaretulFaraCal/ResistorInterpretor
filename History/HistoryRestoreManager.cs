using ResistorInterpretor.Contracts;
using ResistorInterpretor.Logic;

namespace ResistorInterpretor.History
{
    public class HistoryRestoreManager(
        IMainFormUI mainForm,
        IComboBoxManager unitsComboBox,
        IComboBoxManager bandsComboBox,
        IComboBoxManager toleranceComboBox,
        IComboBoxManager tempCoeffComboBox,
        IComboBoxManager[] colorBandComboBoxes,
        ValueConverterLogic logic,
        IRadioButtonManager radioButtonManager,
        IColorConverterLogic colorLogic) : IHistoryRestoreManager
    {
        public void RestoreValueToColorSettings(ValueToColorHistoryEntry entry)
        {
            mainForm.SetResistanceValue(entry.Value);

            // Find and select the unit in the units combo box
            for (int i = 0; i < unitsComboBox.GetItemCount(); i++)
            {
                if (unitsComboBox.GetItemAt(i).ToString() == entry.Unit)
                {
                    unitsComboBox.SetSelectedIndex(i);
                    break;
                }
            }

            // Restore band count - find and select the appropriate item
            radioButtonManager.SetBandCount(entry.BandCount);

            // Restore tolerance color if present
            if (!string.IsNullOrEmpty(entry.ToleranceColor))
            {
                for (int i = 0; i < toleranceComboBox.GetItemCount(); i++)
                {
                    var item = toleranceComboBox.GetItemAt(i).ToString();
                    if (item.Contains(entry.ToleranceColor))
                    {
                        toleranceComboBox.SetSelectedIndex(i);
                        break;
                    }
                }
            }

            // Restore temp coefficient color if present
            if (!string.IsNullOrEmpty(entry.TempCoefficientColor))
            {
                for (int i = 0; i < tempCoeffComboBox.GetItemCount(); i++)
                {
                    var item = tempCoeffComboBox.GetItemAt(i).ToString();
                    if (item.Contains(entry.TempCoefficientColor))
                    {
                        tempCoeffComboBox.SetSelectedIndex(i);
                        break;
                    }
                }
            }

            // Perform the conversion
            logic.Convert(suppressHistory: true);
        }

        public void RestoreColorToValueSettings(ColorToValueHistoryEntry entry)
        {
            // Set the band count first - find and select the appropriate item
            for (int i = 0; i < bandsComboBox.GetItemCount(); i++)
            {
                var item = bandsComboBox.GetItemAt(i).ToString();
                if (item.StartsWith(entry.BandCount.ToString()))
                {
                    bandsComboBox.SetSelectedIndex(i);
                    break;
                }
            }

            // Restore color band selections
            for (int i = 0; i < entry.ColorBandNames.Count && i < colorBandComboBoxes.Length; i++)
            {
                var colorName = entry.ColorBandNames[i];
                var comboBox = colorBandComboBoxes[i];

                for (int j = 0; j < comboBox.GetItemCount(); j++)
                {
                    var item = comboBox.GetItemAt(j).ToString();
                    if (item.Equals(colorName, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox.SetSelectedIndex(j);
                        break;
                    }
                }
            }

            // Perform the conversion
            colorLogic.Convert();
        }
    }
}