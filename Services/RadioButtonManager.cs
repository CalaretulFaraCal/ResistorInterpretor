using Microsoft.VisualBasic.Logging;
using ResistorInterpretor.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistorInterpretor.Services
{
    public class RadioButtonManager : IRadioButtonManager
    {
        private readonly RadioButton rb1;
        private readonly RadioButton rb2;
        private readonly RadioButton rb3;
        private readonly RadioButton rb4;
        private readonly IValueConverterLogic logic;
        private readonly IComboBoxManager comboBoxManageTolerance;
        private readonly IComboBoxManager comboBoxManageTempCoeff;

        public RadioButtonManager(RadioButton rb1, RadioButton rb2, RadioButton rb3, RadioButton rb4, IValueConverterLogic logic, IComboBoxManager comboBoxManageTolerance, IComboBoxManager comboBoxManageTempCoeff)
        {
            this.rb1 = rb1;
            this.rb2 = rb2;
            this.rb3 = rb3;
            this.rb4 = rb4;
            this.logic = logic;
            this.comboBoxManageTolerance = comboBoxManageTolerance;
            this.comboBoxManageTempCoeff = comboBoxManageTempCoeff;

            rb1.Checked = true;
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
