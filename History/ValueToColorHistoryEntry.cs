namespace ResistorInterpretor.History
{
    public class ValueToColorHistoryEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Input parameters for restoration - using property initialization like you prefer
        public double Value { get; set; }
        public string Unit { get; set; } = "Ohm";
        public int BandCount { get; set; }
        public string? ToleranceColor { get; set; }
        public string? TempCoefficientColor { get; set; }

        // Display properties
        public string DisplayValue => Unit switch
        {
            "kOhm" => $"{Value} kΩ",
            "MOhm" => $"{Value} MΩ",
            "GOhm" => $"{Value} GΩ",
            _ => $"{Value} Ω"
        };

        public string DisplaySettings => $"{BandCount} bands" +
                                         (ToleranceColor != null ? $", {ToleranceColor} tolerance" : "") +
                                         (TempCoefficientColor != null ? $", {TempCoefficientColor} temp" : "");
    }
}