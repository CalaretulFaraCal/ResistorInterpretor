using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Filter by units
            if (CurrentFilterValue is "Ohm" or "kOhm" or "MOhm" or "GOhm")
                return entries.Where(e => e.Unit == CurrentFilterValue);

            // Filter by bands (e.g., "3 bands" → 3)
            if (CurrentFilterValue.EndsWith("bands"))
            {
                var bandCount = int.Parse(CurrentFilterValue[0].ToString());
                return entries.Where(e => e.BandCount == bandCount);
            }

            // If not recognized, just return all
            return entries;
        }

        public IEnumerable<string> GetAvailableFilterTypes() =>
            new[] { "None", "Unit", "Bands" };

        public IEnumerable<string> GetAvailableFilterValues(string filterType)
        {
            if (filterType == "Unit")
                return new[] { "Ohm", "kOhm", "MOhm", "GOhm" };
            return Array.Empty<string>();
        }

        public void ResetFilter()
        {
            CurrentFilterType = "None";
            CurrentFilterValue = null;
        }
    }
}
