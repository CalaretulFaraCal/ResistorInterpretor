using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IValueToColorHistoryDisplay
    {
        void RefreshDisplay();
        void ClearDisplay();
        event EventHandler<ValueToColorHistoryEntry>? EntrySelected;
    }
}