using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IFilterManager
    {
        IEnumerable<ValueToColorHistoryEntry> ApplyFilter(IEnumerable<ValueToColorHistoryEntry> entries);
        IEnumerable<string> GetAvailableFilterTypes();
        IEnumerable<string> GetAvailableFilterValues(string filterType);
        string CurrentFilterType { get; set; }
        string CurrentFilterValue { get; set; }
        void ResetFilter();
    }
}
