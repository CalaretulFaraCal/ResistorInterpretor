namespace ResistorInterpretor.Contracts
{
    public interface IGenerateBandsManager
    {
        List<(Color Color, string Label)> FromValue(
            string significantDigits,
            int multiplierIndex,
            int bandCount,
            string? toleranceColor,
            string? tempCoeffColor);

        List<(Color Color, string Label)> FromColorNames(
            List<string> colorNames,
            int bandCount);
    }
}
