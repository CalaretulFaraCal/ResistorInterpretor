namespace ResistorInterpretor
{
    public partial class Form1 : Form
    {
        Color[] Colors = { Color.Black, Color.Brown, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet, Color.Gray, Color.White, Color.Gold, Color.Silver };

        Dictionary<string, (Color color, double tolerance, int tempCoeff)> ColorData = new()
        {
            ["Black"] = (Color.Black, 0, 250),
            ["Brown"] = (Color.Brown, 1, 100),
            ["Red"] = (Color.Red, 2, 50),
            ["Orange"] = (Color.Orange, 0, 15),
            ["Yellow"] = (Color.Yellow, 0, 25),
            ["Green"] = (Color.Green, 0.5, 15),
            ["Blue"] = (Color.Blue, 0.25, 10),
            ["Violet"] = (Color.Violet, 0.1, 5),
            ["Gray"] = (Color.Gray, 0.05, 1),
            ["White"] = (Color.White, 0, 0),
            ["Gold"] = (Color.Gold, 5, 0),
            ["Silver"] = (Color.Silver, 10, 0)
        };

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Band", 160);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.TrimStart('0');
            int bandCount = GetBandCount();
            int sigDigits = bandCount >= 5 ? 3 : 2;

            if (input.Length < sigDigits || input.Length > sigDigits + 9)
            {
                MessageBox.Show("Invalid input length.");
                return;
            }

            string sigPart = input.Substring(0, sigDigits);
            int multiplier = input.Length - sigDigits;

            listView1.Items.Clear();

            foreach (char c in sigPart)
                AddBand(Colors[c - '0']);

            if (multiplier >= Colors.Length)
            {
                MessageBox.Show("Multiplier out of range.");
                return;
            }
            else AddBand(Colors[multiplier]);


            if (bandCount > 3) AddTolerance();
            if (bandCount == 6) AddTempCoeff();
        }

        private int GetBandCount()
        {
            if (radioButton2.Checked) return 4;
            if (radioButton3.Checked) return 5;
            if (radioButton4.Checked) return 6;
            return 3;
        }

        private void AddTolerance()
        {
            int bandCount = GetBandCount();
            string toleranceColor;

            if (bandCount >= 5)
                toleranceColor = "Brown"; // ±1% for precision resistors
            else toleranceColor = "Gold"; // ±5% for standard resistors

            var tol = ColorData[toleranceColor];
            AddBand(tol.color, $"{toleranceColor} (±{tol.tolerance}%)"); ;
        }

        private void AddTempCoeff()
        {
            string tempColor = "Brown"; 
            var temp = ColorData[tempColor];
            AddBand(temp.color, $"{tempColor} ({temp.tempCoeff}ppm/K)");
        }

        private void AddBand(Color color, string text = null)
        {
            listView1.Items.Add(new ListViewItem(text ?? color.Name)
            {
                BackColor = color,
                ForeColor = GetTextColor(color)
            });
        }

        private Color GetTextColor(Color bg)
        {
            int brightness = (int)Math.Sqrt(bg.R * bg.R * 0.241 + bg.G * bg.G * 0.691 + bg.B * bg.B * 0.068);
            return brightness < 130 ? Color.White : Color.Black;
        }
    }
}