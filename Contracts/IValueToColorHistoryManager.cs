using ResistorInterpretor.History;

namespace ResistorInterpretor.Contracts
{
    public interface IValueToColorHistoryManager
    {
        void SaveEntry(double value, string unit, int bandCount, string? toleranceColor, string? tempCoefficientColor);
        IEnumerable<ValueToColorHistoryEntry> GetRecentEntries(int count = 20);
        IEnumerable<ValueToColorHistoryEntry> GetAllEntries();
        void ClearHistory();
        bool RemoveEntry(string id);
    }
}