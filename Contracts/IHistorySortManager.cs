using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IHistorySortManager
    {
        IEnumerable<ValueToColorHistoryEntry> Sort(
            IEnumerable<ValueToColorHistoryEntry> entries, string sortBy);
    }
}