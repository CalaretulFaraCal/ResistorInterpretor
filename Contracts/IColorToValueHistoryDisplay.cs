using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IColorToValueHistoryDisplay
    {
        void RefreshDisplay();
        void ClearDisplay();
        event EventHandler<ColorToValueHistoryEntry>? EntrySelected;
    }
}