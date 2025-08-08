using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface ISortManager
    {
        IEnumerable<ValueToColorHistoryEntry> SortVTC(
            IEnumerable<ValueToColorHistoryEntry> entries, string sortBy);
        IEnumerable<ColorToValueHistoryEntry> SortCTV(
            IEnumerable<ColorToValueHistoryEntry> entries, string sortBy);
    }
}