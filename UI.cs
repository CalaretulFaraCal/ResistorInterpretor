using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ResistorInterpretor
{
    public static class UI
    {
        // ListView Configuration
        public static void SetupListView(ListView listView)
        {
            listView.View = View.Details;
            listView.Columns.Clear();
            listView.Columns.Add("Band", 160);
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.FullRowSelect = false;
            listView.GridLines = false;
        }

        public static void AddBand(ListView listView, Color color, string text = null)
        {
            var item = new ListViewItem(text ?? color.Name)
            {
                BackColor = color,
                ForeColor = GetTextColor(color)
            };
            listView.Items.Add(item);
        }

        // ComboBox Configuration
        public static void PopulateUnitComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(new object[] { "Ohm", "kOhm", "MOhm" });
            comboBox.SelectedIndex = 0;
        }

        public static void PopulateComboBox(ComboBox comboBox, string propertyType)
        {
            string previousSelection = comboBox.SelectedItem?.ToString();
            comboBox.Items.Clear();

            if (propertyType == "bands")
            {
                comboBox.Items.AddRange(new object[] { "3 bands", "4 bands", "5 bands", "6 bands" });
            }
            else if (propertyType == "tolerance")
            {
                foreach (var c in ResistorColorInfo.AllColors)
                {
                    if (c.Tolerance.HasValue)
                    {
                        comboBox.Items.Add($"±{c.Tolerance}% ({c.Name})");
                    }
                }
            }
            else if (propertyType == "temperatureCoefficient")
            {
                foreach (var c in ResistorColorInfo.AllColors)
                {
                    if (c.TemperatureCoefficient.HasValue)
                    {
                        comboBox.Items.Add($"{c.TemperatureCoefficient}ppm/K ({c.Name})");
                    }
                }
            }

            RestoreComboBoxSelection(comboBox, previousSelection);
        }

        public static void RestoreComboBoxSelection(ComboBox comboBox, string previousSelection)
        {
            if (previousSelection != null)
            {
                int previousIndex = comboBox.Items.IndexOf(previousSelection);
                comboBox.SelectedIndex = previousIndex >= 0 ? previousIndex : 0;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        public static string GetSelectedColor(ComboBox comboBox, string propertyType, string defaultColor)
        {
            if (comboBox.Visible && comboBox.SelectedIndex >= 0)
            {
                string selectedText = comboBox.SelectedItem.ToString();
                int startIndex = selectedText.IndexOf('(');
                int endIndex = selectedText.IndexOf(')');
                if (startIndex >= 0 && endIndex > startIndex)
                    return selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);
            }
            return defaultColor;
        }

        // Visibility Management
        public static void UpdateComboBoxVisibility(ComboBox comboBox1, ComboBox comboBox2, int bandCount, int previousBandCount)
        {
            comboBox1.Visible = bandCount > 3;
            comboBox2.Visible = bandCount == 6;

            if (comboBox1.Visible && bandCount != previousBandCount)
                PopulateComboBox(comboBox1, "tolerance");
            if (comboBox2.Visible && bandCount != previousBandCount)
                PopulateComboBox(comboBox2, "temperatureCoefficient");
        }

        public static void UpdateLabelVisibility(Label label1, Label label2, int bandCount)
        {
            label1.Visible = bandCount > 3;
            label2.Visible = bandCount == 6;
        }

        // Color Selection and RadioButton Management
        public static List<ResistorColorInfo> GetSelectedColors(List<FlowLayoutPanel> panels)
        {
            var selected = new List<ResistorColorInfo>();

            foreach (var flp in panels)
            {
                var rb = flp.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                if (rb != null && rb.Tag is ResistorColorInfo colorInfo)
                {
                    selected.Add(colorInfo);
                }
            }

            return selected;
        }

        public static List<RadioButton> CreateColorButtons(string bandType, EventHandler onCheckedChanged = null)
        {
            var buttons = new List<RadioButton>();
            var colors = ColorToValueConverter.GetValidColors(bandType);

            foreach (var colorInfo in colors)
            {
                var rb = new RadioButton
                {
                    Text = colorInfo.Name,
                    BackColor = colorInfo.Color,
                    AutoSize = true,
                    Tag = colorInfo,
                    ForeColor = GetTextColor(colorInfo.Color)
                };

                if (onCheckedChanged != null)
                    rb.CheckedChanged += onCheckedChanged;

                buttons.Add(rb);
            }

            return buttons;
        }

        // FlowLayoutPanel Management
        public static List<FlowLayoutPanel> PopulateFlowPanels(Control container, int bandCount, EventHandler radioChangedHandler)
        {
            var flowPanels = new List<FlowLayoutPanel>();

            for (int i = 0; i < bandCount; i++)
            {
                var flp = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Margin = new Padding(10)
                };

                var label = new Label
                {
                    Text = ColorToValueConverter.BandLabelTexts[bandCount - 3][i],
                    AutoSize = true
                };

                flp.Controls.Add(label);

                string bandType = ColorToValueConverter.GetBandType(i, bandCount);
                var validColors = ColorToValueConverter.GetValidColors(bandType);

                foreach (var colorInfo in validColors)
                {
                    var rb = new RadioButton
                    {
                        Text = colorInfo.Name,
                        BackColor = colorInfo.Color,
                        Tag = colorInfo,
                        AutoSize = true
                    };
                    rb.CheckedChanged += radioChangedHandler;
                    flp.Controls.Add(rb);
                }

                container.Controls.Add(flp);
                flowPanels.Add(flp);
            }

            return flowPanels;
        }

        public static void UpdateFlowPanelPositions(FlowLayoutPanel[] flowPanels, int bandCount, TableLayoutPanel layout)
        {
            layout.Controls.Clear();

            if (bandCount == 3 || bandCount == 4)
            {
                for (int i = 0; i < bandCount; i++)
                {
                    layout.Controls.Add(flowPanels[i], 0, i);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    layout.Controls.Add(flowPanels[i], 0, i);

                layout.Controls.Add(flowPanels[3], 0, 3);
                layout.Controls.Add(flowPanels[4], 0, 4);

                if (bandCount == 6)
                    layout.Controls.Add(flowPanels[5], 0, 5);
            }
        }

        public static void UpdateFlowPanelPositions(List<FlowLayoutPanel> flowPanels, int startX = 10, int startY = 80, int spacingX = 20, int spacingY = 10)
        {
            int y = startY;

            foreach (var panel in flowPanels)
            {
                panel.Location = new Point(startX, y);
                y += panel.Height + spacingY;
            }
        }

        // Utility Methods
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private static Color GetTextColor(Color backgroundColor)
        {
            int brightness = (int)Math.Sqrt(
                backgroundColor.R * backgroundColor.R * 0.241 +
                backgroundColor.G * backgroundColor.G * 0.691 +
                backgroundColor.B * backgroundColor.B * 0.068);

            return brightness < 130 ? Color.White : Color.Black;
        }
    }
}