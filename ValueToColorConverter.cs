using System;
using System.Linq;
using System.Windows.Forms;

namespace ResistorInterpretor
{
    public partial class ValueToColorConverter : Form
    {
        private int previousBandCount = 3;

        public ValueToColorConverter()
        {
            InitializeComponent();

            radioButton1.Checked = true;
            label1.Visible = false;
            label2.Visible = false;

            UI.PopulateComboBox(comboBox4, "bands");
            comboBox4.SelectedIndex = previousBandCount - 3; 

            foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
                rb.CheckedChanged += (_, _) => UpdateBandCount();

            UI.SetupListView(listView1);
            UI.PopulateUnitComboBox(comboBox3);

            UI.UpdateComboBoxVisibility(comboBox1, comboBox2, previousBandCount, -1);

        }

        private void UpdateBandCount()
        {
            int bandCount = radioButton2.Checked ? 4 :
                            radioButton3.Checked ? 5 :
                            radioButton4.Checked ? 6 : 3;

            if (bandCount == previousBandCount)
                return;

            // Actualizează vizibilitatea comboBox-urilor
            UI.UpdateComboBoxVisibility(comboBox1, comboBox2, bandCount, previousBandCount);

            // Actualizează vizibilitatea label-urilor
            UI.UpdateLabelVisibility(label1, label2, bandCount);
            previousBandCount = bandCount;

            // *** Apelează re-populare pentru comboBox-urile care țin de toleranță și temp coef
            if (bandCount > 3)
            {
                UI.PopulateComboBox(comboBox1, "tolerance");
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox1.Visible = false;
            }

            if (bandCount == 6)
            {
                UI.PopulateComboBox(comboBox2, "temperatureCoefficient");
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Visible = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double value) || value <= 0)
            {
                UI.ShowMessage("Enter a valid number.");
                return;
            }

            switch (comboBox3.SelectedItem?.ToString())
            {
                case "kOhm": value *= 1_000; break;
                case "MOhm": value *= 1_000_000; break;
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

            GenerateBands(significantDigits, multiplierIndex, bandCount);
        }

        private void GenerateBands(string significantDigits, int multiplierIndex, int bandCount)
        {
            listView1.Clear();

            // Add significant digit bands
            foreach (char c in significantDigits)
            {
                int digit = c - '0';
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(ci => ci.Digit == digit);
                if (colorInfo != null)
                    UI.AddBand(listView1, colorInfo.Color, colorInfo.Name);
            }

            // Add multiplier band
            if (multiplierIndex >= 0 && multiplierIndex < 12)
            {
                var multiplierColor = ResistorColorInfo.AllColors[multiplierIndex];
                UI.AddBand(listView1, multiplierColor.Color, multiplierColor.Name);
            }
            // Add tolerance band (4+ bands)
            if (bandCount > 3)
            {
                string colorName = UI.GetSelectedColor(comboBox1, "tolerance", "Gold");
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
                if (colorInfo?.Tolerance.HasValue == true)
                    UI.AddBand(listView1, colorInfo.Color, $"{colorName} (±{colorInfo.Tolerance}%)");
            }

            // Add temperature coefficient band (6 bands only)
            if (bandCount == 6)
            {
                string colorName = UI.GetSelectedColor(comboBox2, "temperatureCoefficient", "Brown");
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
                if (colorInfo?.TemperatureCoefficient.HasValue == true)
                    UI.AddBand(listView1, colorInfo.Color, $"{colorName} ({colorInfo.TemperatureCoefficient}ppm/K)");
            }
        }
       
    }
}