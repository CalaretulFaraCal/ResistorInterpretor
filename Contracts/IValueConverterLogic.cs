namespace ResistorInterpretor.Contracts
{
    public interface IValueConverterLogic
    {
        int previousBandCount { get; set; }
        void Convert(bool suppressHistory);
        void UpdateToleranceVisibility(int bandCount);
        void UpdateTemperatureCoefficientVisibility(int bandCount);
    }
}