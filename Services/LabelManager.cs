using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services
{
    public class LabelManager(Label label) : ILabelManager
    {
        public void SetVisibility(bool visible)
        {
                label.Visible = visible;
        }

        public void UpdateLabelVisibility(string propertyType, int bandCount)
        {
            label.Visible = propertyType switch
            {
                "tolerance" => bandCount > 3,
                "temperatureCoefficient" => bandCount == 6,
                "comboBox3" => bandCount >= 5,
                "comboBox5" => bandCount >= 4,
                "comboBox6" => bandCount == 6,
                _ => label.Visible
            };
        }
    }
}