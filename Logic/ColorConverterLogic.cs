using System;
using System.Collections.Generic;
using System.Linq;

namespace ResistorInterpretor.Logic
{
    public static class ColorConverterLogic
    {
        public const int defaultBandCount = 3;

        internal static readonly string[][] BandLabelTexts = new[]
        {
            new[] { "1st band color", "2nd band color", "Multiplier" },
            new[] { "1st band color", "2nd band color", "Multiplier", "Tolerance" },
            new[] { "1st band color", "2nd band color", "3rd band color", "Multiplier", "Tolerance" },
            new[] { "1st band color", "2nd band color", "3rd band color", "Multiplier", "Tolerance", "Temp. Coefficient" }
        };

        public static string GetBandType(int bandIndex, int totalBands)
        {
            if (bandIndex < totalBands - 2) return "digit";
            if (bandIndex == totalBands - 2) return "multiplier";

            if (bandIndex == totalBands - 1)
            {
                if (totalBands == 6) return "temperatureCoefficient";
                if (totalBands > 3) return "tolerance";
            }

            return "all";
        }

        public static List<ResistorColorInfo> GetValidColors(string bandType)
        {
            return ResistorColorInfo.AllColors.Where(c =>
            {
                if (bandType == "digit") return c.Digit.HasValue;
                if (bandType == "multiplier") return c.MultiplierExponent.HasValue;
                if (bandType == "tolerance") return c.Tolerance.HasValue;
                if (bandType == "temperatureCoefficient") return c.TemperatureCoefficient.HasValue;
                return true;
            }).ToList();
        }


        public static double ConvertToValue(List<ResistorColorInfo> colors, string unit = "Ohm")
        {
            if (colors == null || colors.Count < 3)
                throw new ArgumentException("Minimum 3 bands required");

            if (!colors[colors.Count - 2].MultiplierExponent.HasValue)
                throw new ArgumentException("Multiplier band must have a valid multiplier value");

            double value;

            if (colors.Count <= 4)
            {
                value = 10 * colors[0].Digit.Value + colors[1].Digit.Value;
                value *= Math.Pow(10, colors[colors.Count - 2].MultiplierExponent.Value);
            }
            else
            {
                value = 100 * colors[0].Digit.Value + 10 * colors[1].Digit.Value + colors[2].Digit.Value;
                value *= Math.Pow(10, colors[colors.Count - 2].MultiplierExponent.Value);
            }

            switch (unit)
            {
                case "kOhm": return value / 1_000;
                case "MOhm": return value / 1_000_000;
                default: return value;
            }
        }

    }
}