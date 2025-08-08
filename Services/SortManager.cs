using System.Text.RegularExpressions;
using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;

namespace ResistorInterpretor.Services
{
    public class SortManager : ISortManager
    {
        public IEnumerable<ValueToColorHistoryEntry> SortVTC(
            IEnumerable<ValueToColorHistoryEntry> entries, string sortBy)
        {
            return sortBy switch
            {
                "Value" => entries.OrderBy(e => ConvertToOhms(e.Value, e.Unit)),
                "Bands" => entries.OrderBy(e => e.BandCount),
                "Tolerance" => entries.OrderBy(e => GetToleranceColorIndex(e.ToleranceColor)),
                "TempCoeff" => entries.OrderBy(e => GetTempCoeffColorIndex(e.TempCoefficientColor)),
                _ => entries
            };
        }

        public IEnumerable<ColorToValueHistoryEntry> SortCTV(
            IEnumerable<ColorToValueHistoryEntry> entries, string sortBy)
        {
            return sortBy switch
            {
                "Band Count" => entries.OrderBy(e => e.BandCount),
                "Color 1" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 0)),
                "Color 2" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 1)),
                "Color 3" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 2)),
                "Color 4" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 3)),
                "Color 5" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 4)),
                "Color 6" => entries.OrderBy(e => GetColorIndex(e.ColorBandNames.Take(e.BandCount).ToList(), 5)),
                "Time" => entries.OrderBy(e => e.Timestamp),
                _ => entries
            };
        }

        private int GetColorIndex(List<string> bandNames, int bandPosition)
        {
            int maxIndex = ResistorColorInfo.AllColors.Max(c => c.Index) + 1;
            string debugMsg = "";

            if (bandNames == null)
            {
                debugMsg = $"bandNames is null at position {bandPosition}, returning {maxIndex}";
                System.Diagnostics.Debug.WriteLine(debugMsg);
                return maxIndex;
            }

            if (bandNames.Count <= bandPosition)
            {
                debugMsg = $"bandNames.Count ({bandNames.Count}) <= bandPosition ({bandPosition}), returning {maxIndex}";
                System.Diagnostics.Debug.WriteLine(debugMsg);
                return maxIndex;
            }

            var name = bandNames[bandPosition];
            var info = ResistorColorInfo.AllColors.FirstOrDefault(
                c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
            int result = info?.Index ?? maxIndex;

            debugMsg = $"bandNames: [{string.Join(", ", bandNames)}], position: {bandPosition}, name: {name}, foundIndex: {result}";
            System.Diagnostics.Debug.WriteLine(debugMsg);

            return result;
        }

        private double ConvertToOhms(double value, string unit)
        {
            return unit switch
            {
                "Ohm" or "Ω" => value,
                "kOhm" or "kΩ" => value * 1_000,
                "MOhm" or "MΩ" => value * 1_000_000,
                "GOhm" or "GΩ" => value * 1_000_000_000,
                _ => value
            };
        }

        private int GetToleranceColorIndex(string? colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName)) return int.MaxValue;
            var info = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
            return info?.Index ?? int.MaxValue;
        }

        private int GetTempCoeffColorIndex(string? colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName)) return int.MaxValue;
            var info = ResistorColorInfo.AllColors.FirstOrDefault(c => c.Name == colorName);
            return info?.Index ?? int.MaxValue;
        }
    }
}