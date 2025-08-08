using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;

namespace ResistorInterpretor.Services
{
    public class FilterManager : IFilterManager
    {
        public string CurrentFilterType { get; set; } = "None";
        public string CurrentFilterValue { get; set; }
        public IEnumerable<ValueToColorHistoryEntry> ApplyFilter(IEnumerable<ValueToColorHistoryEntry> entries)
        {
            if (string.IsNullOrEmpty(CurrentFilterValue) || CurrentFilterValue == "None")
                return entries;

            if (CurrentFilterValue is "Ohm" or "kOhm" or "MOhm" or "GOhm")
                return entries.Where(e => e.Unit == CurrentFilterValue);

            if (CurrentFilterValue.EndsWith("bands"))
            {
                var bandCount = int.Parse(CurrentFilterValue[0].ToString());
                return entries.Where(e => e.BandCount == bandCount);
            }

            return entries;
        }
        public IEnumerable<ColorToValueHistoryEntry> ApplyFilter(IEnumerable<ColorToValueHistoryEntry> entries)
        {
            if (string.IsNullOrEmpty(CurrentFilterValue) || CurrentFilterValue == "None")
                return entries;

            if (CurrentFilterValue.EndsWith("bands"))
            {
                var bandCount = int.Parse(CurrentFilterValue[0].ToString());
                return entries.Where(e => e.BandCount == bandCount);
            }

            // Filter by color presence
            // (Colors are stored as names in ColorBandNames)
            var colorName = CurrentFilterValue;
            return entries.Where(e => e.ColorBandNames.Any(band => band == colorName));
        }
    }
}
