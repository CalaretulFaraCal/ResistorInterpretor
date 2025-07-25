namespace ResistorInterpretor.Contracts
{
    public interface IColorConverterLogic
    {
        void Convert();
        void UpdateBandComboBoxVisibility(int bandCount, int previousBandCount ,IComboBoxManager[] managers);
    }

}
