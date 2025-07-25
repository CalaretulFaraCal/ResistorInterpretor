using ResistorInterpretor.Contracts;
using Label = System.Windows.Forms.Label;

namespace ResistorInterpretor.Services
{
    public class LabelManager(Label label) : ILabelManager
    {
        public void SetVisibility(bool visible)
        {
                label.Visible = visible;
        }

        public void UpdateLabelVisibility(string name, int bandCount)
        {
            if(name.ToLower()=="tolerance")
                SetVisibility(bandCount>3);

            if (name.ToLower() == "temperaturecoefficient")
                SetVisibility(bandCount==6);

        }
    }
}