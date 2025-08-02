using System;
using System.Linq;
using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services
{
    public class RadioButtonManager : IRadioButtonManager
    {
        private readonly RadioButton rb1;
        private readonly RadioButton rb2;
        private readonly RadioButton rb3;
        private readonly RadioButton rb4;
        private readonly IValueConverterLogic logic;
        private bool suppressEvents = false;

        public event EventHandler? BandCountChanged;

        public RadioButtonManager(RadioButton rb1, RadioButton rb2, RadioButton rb3, RadioButton rb4, IValueConverterLogic logic)
        {
            this.rb1 = rb1;
            this.rb2 = rb2;
            this.rb3 = rb3;
            this.rb4 = rb4;
            this.logic = logic;

            rb1.Checked = true;

            rb1.CheckedChanged += OnBandCountChanged;
            rb2.CheckedChanged += OnBandCountChanged;
            rb3.CheckedChanged += OnBandCountChanged;
            rb4.CheckedChanged += OnBandCountChanged;
        }

        private void OnBandCountChanged(object? sender, EventArgs e)
        {
            if (suppressEvents) return;
            BandCountChanged?.Invoke(this, e);
        }

        public int GetValue()
        {
            var buttons = new[] { rb1, rb2, rb3, rb4 };
            var selectedButton = buttons.FirstOrDefault(rb => rb.Checked);
            if (selectedButton == null)
                throw new InvalidOperationException("No band radio button is checked.");
            var digits = new string(selectedButton.Text.Where(char.IsDigit).ToArray());
            return int.Parse(digits);
        }

        public int UpdateBandCount()
        {
            var bandCount = GetValue();

            if (bandCount == logic.previousBandCount)
                return bandCount;

            logic.UpdateToleranceVisibility(bandCount);
            logic.UpdateTemperatureCoefficientVisibility(bandCount);

            logic.previousBandCount = bandCount;

            return bandCount;
        }

        public void SetBandCount(int bandCount)
        {
            suppressEvents = true;
            rb1.Checked = bandCount == 3;
            rb2.Checked = bandCount == 4;
            rb3.Checked = bandCount == 5;
            rb4.Checked = bandCount == 6;
            suppressEvents = false;

            // Manually fire the event ONCE after all buttons are set
            BandCountChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}