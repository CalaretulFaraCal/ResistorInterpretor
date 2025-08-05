namespace ResistorInterpretor.Contracts
{
    public interface IColorConverterLogic
    {
        void Convert(bool suppressHistory);
        void UpdateBandComboBoxVisibility(int bandCount, int previousBandCount ,IComboBoxManager[] managers);
    }

}
