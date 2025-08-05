using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services
{
    public class GenerateBandsManager : IGenerateBandsManager
    {
        public List<(Color Color, string Label)> FromValue(
            string significantDigits,
            int multiplierIndex,
            int bandCount,
            string? toleranceColor,
            string? tempCoeffColor)
        {
            var result = new List<(Color, string)>();

            foreach (var c in significantDigits)
            {
                var digit = c - '0';
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(ci => ci.Digit == digit);
                if (colorInfo != null)
                    result.Add((colorInfo.Color, colorInfo.Name));
            }

            // Add multiplier band
            if (multiplierIndex >= 0 && multiplierIndex < ResistorColorInfo.AllColors.Count)
            {
                var multiplierColor = ResistorColorInfo.AllColors[multiplierIndex];
                result.Add((multiplierColor.Color, multiplierColor.Name));
            }

            // Add tolerance band (if needed)
            if (bandCount > 3 && !string.IsNullOrEmpty(toleranceColor))
            {
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == toleranceColor);
                if (colorInfo?.Tolerance.HasValue == true)
                    result.Add((colorInfo.Color, $"{colorInfo.Name} (±{colorInfo.Tolerance}%)"));
            }

            // Add temperature coefficient band (if needed)
            if (bandCount == 6 && !string.IsNullOrEmpty(tempCoeffColor))
            {
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == tempCoeffColor);
                if (colorInfo?.TemperatureCoefficient.HasValue == true)
                    result.Add((colorInfo.Color, $"{colorInfo.Name} ({colorInfo.TemperatureCoefficient}ppm/K)"));
            }

            return result;
        }

        public List<(Color Color, string Label)> FromColorNames(
            List<string> colorNames,
            int bandCount)
        {
            var result = new List<(Color, string)>();

            for (int i = 0; i < bandCount && i < colorNames.Count; i++)
            {
                var colorInfo = ResistorColorInfo.AllColors.FirstOrDefault(ci => ci.Name == colorNames[i]);
                if (colorInfo != null)
                    result.Add((colorInfo.Color, colorInfo.Name));
            }
            return result;
        }
    }
}