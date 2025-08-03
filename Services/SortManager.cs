using System.Text.RegularExpressions;
using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;

namespace ResistorInterpretor.Services
{
    public class SortManager : ISortManager
    {
        public IEnumerable<ValueToColorHistoryEntry> Sort(
            IEnumerable<ValueToColorHistoryEntry> entries, string sortBy)
        {
            return sortBy switch
            {
                "Value" => entries.OrderBy(e => ConvertToOhms(e.Value, e.Unit)),
                "Bands" => entries.OrderBy(e => e.BandCount),
                "Tolerance" => entries.OrderBy(e => ParseTolerance(e.ToleranceColor)),
                "TempCoeff" => entries.OrderBy(e => ParseTempCoeff(e.TempCoefficientColor)),
                _ => entries
            };
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

        private double ParseTolerance(string? tolerance)
        {
            if (string.IsNullOrWhiteSpace(tolerance)) return 100.0;
            var match = Regex.Match(tolerance, @"([\d.]+)%");
            return match.Success ? double.Parse(match.Groups[1].Value) : 100.0;
        }

        private int ParseTempCoeff(string? tempCoeff)
        {
            if (string.IsNullOrWhiteSpace(tempCoeff)) return int.MaxValue;
            var match = Regex.Match(tempCoeff, @"(\d+)ppm");
            return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
        }
    }
}