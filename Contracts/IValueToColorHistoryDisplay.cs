using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IValueToColorHistoryDisplay
    {
        event EventHandler<ValueToColorHistoryEntry>? EntrySelected;

        void RefreshDisplay();
        void ClearDisplay();
        void RefreshDisplay(IEnumerable<ValueToColorHistoryEntry> entries);

    }
}