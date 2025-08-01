namespace ResistorInterpretor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            inputValue = new TextBox();
            listViewVTC = new ListView();
            button1 = new Button();
            radioButtonBands_3 = new RadioButton();
            radioButtonBands_4 = new RadioButton();
            radioButtonBands_5 = new RadioButton();
            radioButtonBands_6 = new RadioButton();
            comboBoxTolerance = new ComboBox();
            comboBoxTempCoeff = new ComboBox();
            comboBoxUnits = new ComboBox();
            tabValueToColor = new TabControl();
            tabPage1 = new TabPage();
            TemperatureCoefficient = new Label();
            Tolerance = new Label();
            tabPage2 = new TabPage();
            button2 = new Button();
            comboBox6 = new ComboBox();
            comboBox5 = new ComboBox();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            labelResult = new Label();
            comboBoxBands = new ComboBox();
            listView1 = new ListView();
            tabValueToColor.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // inputValue
            // 
            inputValue.Location = new Point(94, 79);
            inputValue.Name = "inputValue";
            inputValue.Size = new Size(100, 23);
            inputValue.TabIndex = 0;
            // 
            // listViewVTC
            // 
            listViewVTC.Location = new Point(526, 79);
            listViewVTC.Name = "listViewVTC";
            listViewVTC.Size = new Size(180, 156);
            listViewVTC.TabIndex = 2;
            listViewVTC.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            button1.Location = new Point(304, 80);
            button1.Name = "button1";
            button1.Size = new Size(100, 25);
            button1.TabIndex = 3;
            button1.Text = "Calculeaza";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // radioButtonBands_3
            // 
            radioButtonBands_3.AutoSize = true;
            radioButtonBands_3.Location = new Point(108, 132);
            radioButtonBands_3.Name = "radioButtonBands_3";
            radioButtonBands_3.Size = new Size(66, 19);
            radioButtonBands_3.TabIndex = 4;
            radioButtonBands_3.TabStop = true;
            radioButtonBands_3.Text = "3 bands";
            radioButtonBands_3.UseVisualStyleBackColor = true;
            // 
            // radioButtonBands_4
            // 
            radioButtonBands_4.AutoSize = true;
            radioButtonBands_4.Location = new Point(108, 157);
            radioButtonBands_4.Name = "radioButtonBands_4";
            radioButtonBands_4.Size = new Size(66, 19);
            radioButtonBands_4.TabIndex = 5;
            radioButtonBands_4.TabStop = true;
            radioButtonBands_4.Text = "4 bands";
            radioButtonBands_4.UseVisualStyleBackColor = true;
            // 
            // radioButtonBands_5
            // 
            radioButtonBands_5.AutoSize = true;
            radioButtonBands_5.Location = new Point(108, 182);
            radioButtonBands_5.Name = "radioButtonBands_5";
            radioButtonBands_5.Size = new Size(66, 19);
            radioButtonBands_5.TabIndex = 6;
            radioButtonBands_5.TabStop = true;
            radioButtonBands_5.Text = "5 bands";
            radioButtonBands_5.UseVisualStyleBackColor = true;
            // 
            // radioButtonBands_6
            // 
            radioButtonBands_6.AutoSize = true;
            radioButtonBands_6.Location = new Point(108, 207);
            radioButtonBands_6.Name = "radioButtonBands_6";
            radioButtonBands_6.Size = new Size(66, 19);
            radioButtonBands_6.TabIndex = 7;
            radioButtonBands_6.TabStop = true;
            radioButtonBands_6.Text = "6 bands";
            radioButtonBands_6.UseVisualStyleBackColor = true;
            // 
            // comboBoxTolerance
            // 
            comboBoxTolerance.FormattingEnabled = true;
            comboBoxTolerance.Location = new Point(200, 132);
            comboBoxTolerance.Name = "comboBoxTolerance";
            comboBoxTolerance.Size = new Size(123, 23);
            comboBoxTolerance.TabIndex = 8;
            // 
            // comboBoxTempCoeff
            // 
            comboBoxTempCoeff.FormattingEnabled = true;
            comboBoxTempCoeff.Location = new Point(329, 132);
            comboBoxTempCoeff.Name = "comboBoxTempCoeff";
            comboBoxTempCoeff.Size = new Size(135, 23);
            comboBoxTempCoeff.TabIndex = 9;
            // 
            // comboBoxUnits
            // 
            comboBoxUnits.FormattingEnabled = true;
            comboBoxUnits.Location = new Point(200, 80);
            comboBoxUnits.Name = "comboBoxUnits";
            comboBoxUnits.Size = new Size(72, 23);
            comboBoxUnits.TabIndex = 10;
            // 
            // tabValueToColor
            // 
            tabValueToColor.AccessibleName = "";
            tabValueToColor.Controls.Add(tabPage1);
            tabValueToColor.Controls.Add(tabPage2);
            tabValueToColor.Location = new Point(1, 0);
            tabValueToColor.Name = "tabValueToColor";
            tabValueToColor.SelectedIndex = 0;
            tabValueToColor.Size = new Size(990, 646);
            tabValueToColor.TabIndex = 11;
            tabValueToColor.Tag = "";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listView1);
            tabPage1.Controls.Add(TemperatureCoefficient);
            tabPage1.Controls.Add(Tolerance);
            tabPage1.Controls.Add(inputValue);
            tabPage1.Controls.Add(comboBoxUnits);
            tabPage1.Controls.Add(listViewVTC);
            tabPage1.Controls.Add(comboBoxTempCoeff);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(comboBoxTolerance);
            tabPage1.Controls.Add(radioButtonBands_3);
            tabPage1.Controls.Add(radioButtonBands_6);
            tabPage1.Controls.Add(radioButtonBands_4);
            tabPage1.Controls.Add(radioButtonBands_5);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(982, 618);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Value to Color";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // TemperatureCoefficient
            // 
            TemperatureCoefficient.AutoSize = true;
            TemperatureCoefficient.Location = new Point(329, 114);
            TemperatureCoefficient.Name = "TemperatureCoefficient";
            TemperatureCoefficient.Size = new Size(135, 15);
            TemperatureCoefficient.TabIndex = 12;
            TemperatureCoefficient.Text = "Temperature Coefficient";
            // 
            // Tolerance
            // 
            Tolerance.AutoSize = true;
            Tolerance.Location = new Point(236, 114);
            Tolerance.Name = "Tolerance";
            Tolerance.Size = new Size(58, 15);
            Tolerance.TabIndex = 11;
            Tolerance.Text = "Tolerance";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(comboBox6);
            tabPage2.Controls.Add(comboBox5);
            tabPage2.Controls.Add(comboBox4);
            tabPage2.Controls.Add(comboBox3);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(labelResult);
            tabPage2.Controls.Add(comboBoxBands);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(982, 618);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Color to Value";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(192, 28);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 22;
            button2.Text = "Calculeaza";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(782, 109);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(121, 23);
            comboBox6.TabIndex = 21;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(603, 109);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(121, 23);
            comboBox5.TabIndex = 20;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(454, 109);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(121, 23);
            comboBox4.TabIndex = 19;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(309, 109);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(121, 23);
            comboBox3.TabIndex = 18;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(172, 109);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 17;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(35, 109);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(758, 80);
            label6.Name = "label6";
            label6.Size = new Size(167, 15);
            label6.TabIndex = 15;
            label6.Text = "Temperature Coefficient Color";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(617, 80);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 14;
            label5.Text = "Tolerance Color";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(468, 80);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 13;
            label4.Text = "Multiplier Color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(327, 80);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 12;
            label3.Text = "3rd Band Color";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(192, 80);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 11;
            label2.Text = "2nd Band Color";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 80);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 10;
            label1.Text = "1st Band Color";
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(309, 28);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(39, 15);
            labelResult.TabIndex = 9;
            labelResult.Text = "Result";
            // 
            // comboBoxBands
            // 
            comboBoxBands.FormattingEnabled = true;
            comboBoxBands.Location = new Point(35, 28);
            comboBoxBands.Name = "comboBoxBands";
            comboBoxBands.Size = new Size(121, 23);
            comboBoxBands.TabIndex = 1;
            // 
            // listView1
            // 
            listView1.Location = new Point(94, 267);
            listView1.Name = "listView1";
            listView1.Size = new Size(759, 326);
            listView1.TabIndex = 13;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 646);
            Controls.Add(tabValueToColor);
            Name = "MainForm";
            Text = "Form1";
            tabValueToColor.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox inputValue;
        internal ListView listViewVTC;
        private Button button1;
        private RadioButton radioButtonBands_3;
        private RadioButton radioButtonBands_4;
        private RadioButton radioButtonBands_5;
        private RadioButton radioButtonBands_6;
        private ComboBox comboBoxTolerance;
        private ComboBox comboBoxTempCoeff;
        private ComboBox comboBoxUnits;
        private TabControl tabValueToColor;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ComboBox comboBoxBands;
        private Label TemperatureCoefficient;
        private Label Tolerance;
        private Label labelResult;
        private Label label1;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox comboBox6;
        private ComboBox comboBox5;
        private Button button2;
        private ListView listView1;
    }
}
