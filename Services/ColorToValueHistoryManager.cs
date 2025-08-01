using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;
using Newtonsoft.Json;

namespace ResistorInterpretor.Services
{
    public class ColorToValueHistoryManager : IColorToValueHistoryManager
    {
        private readonly string _filePath = "color_to_value_history.json";
        private List<ColorToValueHistoryEntry> _entries = new();

        public ColorToValueHistoryManager()
        {
            _entries = LoadEntries();
        }

        private List<ColorToValueHistoryEntry> LoadEntries()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<List<ColorToValueHistoryEntry>>(json) ?? new();
                }
            }
            catch (Exception ex)
            {
                UI.ShowMessage($"Error loading color-to-value history: {ex.Message}");
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
                UI.ShowMessage($"Error saving color-to-value history: {ex.Message}");
            }
        }

        public void SaveEntry(int bandCount, List<string> colorBandNames)
        {
            var entry = new ColorToValueHistoryEntry
            {
                BandCount = bandCount,
                ColorBandNames = new List<string>(colorBandNames)
            };

            _entries.Add(entry);
            SaveEntries();
        }

        public IEnumerable<ColorToValueHistoryEntry> GetRecentEntries(int count = 20)
        {
            return _entries.OrderByDescending(e => e.Timestamp).Take(count);
        }

        public IEnumerable<ColorToValueHistoryEntry> GetAllEntries()
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