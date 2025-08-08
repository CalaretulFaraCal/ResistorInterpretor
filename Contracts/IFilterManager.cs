using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IFilterManager
    {
        IEnumerable<ValueToColorHistoryEntry> ApplyFilter(IEnumerable<ValueToColorHistoryEntry> entries);
        IEnumerable<ColorToValueHistoryEntry> ApplyFilter(IEnumerable<ColorToValueHistoryEntry> entries);
        string CurrentFilterType { get; set; }
        string CurrentFilterValue { get; set; }
    }
}
