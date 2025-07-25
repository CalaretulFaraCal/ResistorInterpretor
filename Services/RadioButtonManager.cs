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
            BandCountChanged?.Invoke(this, e);
        }

        public int GetValue()
        {
            var buttons = new[] { rb1, rb2, rb3, rb4 };
            var selectedButton = buttons.First(rb => rb.Checked);
            var text = selectedButton.Text;
            return int.Parse(text[0].ToString());
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
    }
}
