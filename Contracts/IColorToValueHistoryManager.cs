using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IColorToValueHistoryManager
    {
        void SaveEntry(int bandCount, List<string> colorBandNames);
        IEnumerable<ColorToValueHistoryEntry> GetRecentEntries(int count = 20);
        IEnumerable<ColorToValueHistoryEntry> GetAllEntries();
        void ClearHistory();
        bool RemoveEntry(string id);
    }
}