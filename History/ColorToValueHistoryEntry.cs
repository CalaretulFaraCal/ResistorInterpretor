namespace ResistorInterpretor.History
{
    public class ColorToValueHistoryEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Input parameters for restoration
        public int BandCount { get; set; }
        public List<string> ColorBandNames { get; set; } = new();

        // Display properties
        public string DisplayColors => string.Join(" → ", ColorBandNames);
        public string DisplaySettings => $"{BandCount} bands";
    }
}