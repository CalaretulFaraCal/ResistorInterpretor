using System.Globalization;

namespace ResistorInterpretor
{
    public partial class Form1 : Form
    {
        Color[] Colors = { Color.Black, Color.Brown, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet, Color.Gray, Color.White, Color.Gold, Color.Silver };

        Dictionary<string, (Color color, double tolerance, int temperatureCoefficient)> ColorData = new()
        {
            ["Black"] = (Color.Black, 0, 250),
            ["Brown"] = (Color.Brown, 1, 100),
            ["Red"] = (Color.Red, 2, 50),
            ["Orange"] = (Color.Orange, 0.05, 15),
            ["Yellow"] = (Color.Yellow, 0.02, 25),
            ["Green"] = (Color.Green, 0.5, 20),
            ["Blue"] = (Color.Blue, 0.25, 10),
            ["Violet"] = (Color.Violet, 0.1, 5),
            ["Gray"] = (Color.Gray, 0.01, 1),
            ["White"] = (Color.White, 0, 0),
            ["Gold"] = (Color.Gold, 5, 0),
            ["Silver"] = (Color.Silver, 10, 0)
        };

        private int previousBandCount = 3;

        public Form1()
        {
            InitializeComponent();
            SetupRadioButtons();
            SetupListView();
            PopulateUnitComboBox();
            comboBox1.Visible = false;
            comboBox2.Visible = false;
        }

        private void SetupRadioButtons()
        {
            radioButton1.Checked = true;
            radioButton1.CheckedChanged += (_, _) => GetBandCount();
            radioButton2.CheckedChanged += (_, _) => GetBandCount();
            radioButton3.CheckedChanged += (_, _) => GetBandCount();
            radioButton4.CheckedChanged += (_, _) => GetBandCount();
        }

        private void SetupListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Band", 160);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double value) || value <= 0)
            {
                MessageBox.Show("Enter a valid number.");
                return;
            }

            switch (comboBox3.SelectedItem?.ToString())
            {
                case "kOhm": value *= 1_000; break;
                case "MOhm": value *= 1_000_000; break;
            }

            int bandCount = GetBandCount();
            int significantDigitCount = bandCount >= 5 ? 3 : 2;

             // Find the best representation with available multipliers
            string significantDigits = "";
            int multiplierIndex = -1;

            // Try different multipliers from Silver (-2) to highest (9)
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

            if (multiplierIndex >= Colors.Length)
            {
                MessageBox.Show("Value out of range.");
                return;
            }

            listView1.Items.Clear();

            foreach (char c in significantDigits)
                AddBand(Colors[c - '0']);

            AddBand(Colors[multiplierIndex]);

            if (bandCount > 3) AddTolerance();
            if (bandCount == 6) AddTemperatureCoefficient();
        }
        private int GetBandCount()
        {
            int bandCount = 3;
            if (radioButton2.Checked) bandCount = 4;
            else if (radioButton3.Checked) bandCount = 5;
            else if (radioButton4.Checked) bandCount = 6;

            UpdateComboBoxVisibility(bandCount);
            return bandCount;
        }

        private void UpdateComboBoxVisibility(int bandCount)
        {
            comboBox1.Visible = bandCount > 3;
            comboBox2.Visible = bandCount == 6;

            if (comboBox1.Visible && bandCount != previousBandCount)
                PopulateComboBox(comboBox1, "tolerance");
            if (comboBox2.Visible && bandCount != previousBandCount)
                PopulateComboBox(comboBox2, "temperatureCoefficient");

            previousBandCount = bandCount;
        }

        private void AddTolerance()
        {
            string colorName = GetSelectedColor(comboBox1, "tolerance", "Gold");
            if (GetBandCount() >= 5 && (!comboBox1.Visible || comboBox1.SelectedIndex < 0))
                colorName = "Brown";

            var colorData = ColorData[colorName];
            AddBand(colorData.color, $"{colorName} (±{colorData.tolerance}%)");
        }

        private void AddTemperatureCoefficient()
        {
            string colorName = GetSelectedColor(comboBox2, "temperatureCoefficient", "Brown");
            var colorData = ColorData[colorName];
            AddBand(colorData.color, $"{colorName} ({colorData.temperatureCoefficient}ppm/K)");
        }

        private void AddBand(Color color, string text = null)
        {
            listView1.Items.Add(new ListViewItem(text ?? color.Name)
            {
                BackColor = color,
                ForeColor = GetTextColor(color)
            });
        }

        private void PopulateComboBox(ComboBox comboBox, string propertyType)
        {
            string previousSelection = comboBox.SelectedItem?.ToString();
            comboBox.Items.Clear();

            foreach (var entry in ColorData)
            {
                double value = propertyType == "tolerance" ? entry.Value.tolerance : entry.Value.temperatureCoefficient;
                if (value > 0)
                {
                    string displayText = propertyType == "tolerance"
                        ? $"±{value}% ({entry.Key})"
                        : $"{value}ppm/K ({entry.Key})";
                    comboBox.Items.Add(displayText);
                }
            }

            RestoreComboBoxSelection(comboBox, previousSelection);
        }

        private void PopulateUnitComboBox()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(new object[] { "Ohm", "kOhm", "MOhm" });
            comboBox3.SelectedIndex = 0;
        }

        private void RestoreComboBoxSelection(ComboBox comboBox, string previousSelection)
        {
            if (previousSelection != null)
            {
                int previousIndex = comboBox.Items.IndexOf(previousSelection);
                comboBox.SelectedIndex = previousIndex >= 0 ? previousIndex : 0;
            }
            else if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        private string GetSelectedColor(ComboBox comboBox, string propertyType, string defaultColor)
        {
            if (comboBox.Visible && comboBox.SelectedIndex >= 0)
            {
                string selectedText = comboBox.SelectedItem.ToString();
                int startIndex = selectedText.IndexOf('(');
                int endIndex = selectedText.IndexOf(')');
                if (startIndex >= 0 && endIndex > startIndex)
                {
                    return selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);
                }
            }
            return defaultColor;
        }

        private Color GetTextColor(Color backgroundColor)
        {
            int brightness = (int)Math.Sqrt(backgroundColor.R * backgroundColor.R * 0.241 + backgroundColor.G * backgroundColor.G * 0.691 + backgroundColor.B * backgroundColor.B * 0.068);
            return brightness < 130 ? Color.White : Color.Black;
        }
    }
}