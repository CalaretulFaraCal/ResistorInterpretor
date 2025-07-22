namespace ResistorInterpretor
{
    partial class Form_
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
            labelTempCoeffVTC = new Label();
            labelToleranceVTC = new Label();
            tabPage2 = new TabPage();
            labelResult = new Label();
            flowLayoutPanel7 = new FlowLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            comboBoxBands = new ComboBox();
            tabValueToColor.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
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
            listViewVTC.Location = new Point(573, 58);
            listViewVTC.Name = "listViewVTC";
            listViewVTC.Size = new Size(197, 311);
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
            tabPage1.Controls.Add(labelTempCoeffVTC);
            tabPage1.Controls.Add(labelToleranceVTC);
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
            // labelTempCoeffVTC
            // 
            labelTempCoeffVTC.AutoSize = true;
            labelTempCoeffVTC.Location = new Point(329, 114);
            labelTempCoeffVTC.Name = "labelTempCoeffVTC";
            labelTempCoeffVTC.Size = new Size(135, 15);
            labelTempCoeffVTC.TabIndex = 12;
            labelTempCoeffVTC.Text = "Temperature Coefficient";
            // 
            // labelToleranceVTC
            // 
            labelToleranceVTC.AutoSize = true;
            labelToleranceVTC.Location = new Point(236, 114);
            labelToleranceVTC.Name = "labelToleranceVTC";
            labelToleranceVTC.Size = new Size(58, 15);
            labelToleranceVTC.TabIndex = 11;
            labelToleranceVTC.Text = "Tolerance";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(labelResult);
            tabPage2.Controls.Add(flowLayoutPanel7);
            tabPage2.Controls.Add(flowLayoutPanel6);
            tabPage2.Controls.Add(flowLayoutPanel5);
            tabPage2.Controls.Add(flowLayoutPanel4);
            tabPage2.Controls.Add(flowLayoutPanel2);
            tabPage2.Controls.Add(flowLayoutPanel1);
            tabPage2.Controls.Add(comboBoxBands);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(982, 618);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Color to Value";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(225, 31);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(39, 15);
            labelResult.TabIndex = 9;
            labelResult.Text = "Result";
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.AutoScroll = true;
            flowLayoutPanel7.AutoSize = true;
            flowLayoutPanel7.Location = new Point(20, 527);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(886, 47);
            flowLayoutPanel7.TabIndex = 8;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoScroll = true;
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.Location = new Point(20, 445);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(886, 47);
            flowLayoutPanel6.TabIndex = 7;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoScroll = true;
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Location = new Point(20, 353);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(886, 47);
            flowLayoutPanel5.TabIndex = 6;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoScroll = true;
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.Location = new Point(20, 259);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(886, 47);
            flowLayoutPanel4.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel2.Location = new Point(20, 177);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(886, 47);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoScroll = true;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Location = new Point(3, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(0, 0);
            flowLayoutPanel3.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Location = new Point(20, 90);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(886, 47);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // comboBoxBands
            // 
            comboBoxBands.FormattingEnabled = true;
            comboBoxBands.Location = new Point(35, 28);
            comboBoxBands.Name = "comboBoxBands";
            comboBoxBands.Size = new Size(121, 23);
            comboBoxBands.TabIndex = 1;
            // 
            // Form_
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 646);
            Controls.Add(tabValueToColor);
            Name = "Form_";
            Text = "Form1";
            tabValueToColor.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
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
        private Label labelTempCoeffVTC;
        private Label labelToleranceVTC;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label labelResult;
        private FlowLayoutPanel flowLayoutPanel7;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
    }
}
