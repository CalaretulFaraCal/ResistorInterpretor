using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface ISortManager
    {
        IEnumerable<ValueToColorHistoryEntry> Sort(
            IEnumerable<ValueToColorHistoryEntry> entries, string sortBy);
    }
}