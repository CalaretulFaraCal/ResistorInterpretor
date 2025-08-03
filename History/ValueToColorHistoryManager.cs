using Newtonsoft.Json;
using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.History
{
    public class ValueToColorHistoryManager : IValueToColorHistoryManager
    {
        private readonly string _filePath = "value_to_color_history.json";
        private List<ValueToColorHistoryEntry> _entries = new();

        public ValueToColorHistoryManager()
        {
            _entries = LoadEntries();
        }

        private List<ValueToColorHistoryEntry> LoadEntries()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<List<ValueToColorHistoryEntry>>(json) ?? new();
                }
            }
            catch (Exception ex)
            {
                UI.ShowMessage($"Error loading value-to-color history: {ex.Message}");
            }
            return new();
        }

        private void SaveEntries()
        {
            try
            {
                var json = JsonConvert.SerializeObject(_entries, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                UI.ShowMessage($"Error saving value-to-color history: {ex.Message}");
            }
        }

        public void SaveEntry(double value, string unit, int bandCount, string? toleranceColor, string? tempCoefficientColor)
        {
            var entry = new ValueToColorHistoryEntry
            {
                Value = value,
                Unit = unit,
                BandCount = bandCount,
                ToleranceColor = toleranceColor,
                TempCoefficientColor = tempCoefficientColor
            };

            _entries.Add(entry);
            SaveEntries();
        }

        public IEnumerable<ValueToColorHistoryEntry> GetRecentEntries(int count = 35)
        {
            return _entries.OrderByDescending(e => e.Timestamp).Take(count);
        }

        public IEnumerable<ValueToColorHistoryEntry> GetAllEntries()
        {
            return _entries.OrderByDescending(e => e.Timestamp);
        }

        public void ClearHistory()
        {
            _entries.Clear();
            SaveEntries();
        }

        public bool RemoveEntry(string id)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _entries.Remove(entry);
                SaveEntries();
                return true;
            }
            return false;
        }
    }
}