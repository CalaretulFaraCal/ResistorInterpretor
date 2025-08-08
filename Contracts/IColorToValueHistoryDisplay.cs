using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IColorToValueHistoryDisplay
    {
        void RefreshDisplay();
        void ClearDisplay();
        void RefreshDisplay(IEnumerable<ColorToValueHistoryEntry> entries);
        event EventHandler<ColorToValueHistoryEntry>? EntrySelected;
    }
}