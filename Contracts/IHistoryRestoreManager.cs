using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IHistoryRestoreManager
    {
        void RestoreValueToColorSettings(ValueToColorHistoryEntry entry);
        void RestoreColorToValueSettings(ColorToValueHistoryEntry entry);
    }
}