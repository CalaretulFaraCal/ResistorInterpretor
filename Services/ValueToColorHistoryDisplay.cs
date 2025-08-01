using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;

namespace ResistorInterpretor.Services
{
    public class ValueToColorHistoryDisplay : IValueToColorHistoryDisplay
    {
        private readonly ListView _listView;
        private readonly IValueToColorHistoryManager _historyManager;
        private readonly IHistoryRestoreManager _restoreManager;

        public event EventHandler<ValueToColorHistoryEntry>? EntrySelected;

        public ValueToColorHistoryDisplay(ListView listView,
            IValueToColorHistoryManager historyManager,
            IHistoryRestoreManager restoreManager)
        {
            _listView = listView;
            _historyManager = historyManager;
            _restoreManager = restoreManager;
            InitializeListView();
        }

        private void InitializeListView()
        {
            _listView.View = View.Details;
            _listView.FullRowSelect = true;
            _listView.GridLines = true;
            _listView.MultiSelect = false;

            _listView.Columns.Clear();
            _listView.Columns.Add("Time", 70);
            _listView.Columns.Add("Value", 100);
            _listView.Columns.Add("Settings", 180);

            _listView.DoubleClick += ListView_DoubleClick;
            RefreshDisplay();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count > 0)
            {
                var entry = (ValueToColorHistoryEntry)_listView.SelectedItems[0].Tag;
                EntrySelected?.Invoke(this, entry);
                _restoreManager.RestoreValueToColorSettings(entry);
            }
        }

        public void RefreshDisplay()
        {
            _listView.BeginUpdate();
            _listView.Items.Clear();

            foreach (var entry in _historyManager.GetRecentEntries(30))
            {
                var item = new ListViewItem(entry.Timestamp.ToString("HH:mm"));
                item.SubItems.Add(entry.DisplayValue);
                item.SubItems.Add(entry.DisplaySettings);
                item.Tag = entry;
                item.ToolTipText = "Double-click to restore settings and calculate";
                _listView.Items.Add(item);
            }

            _listView.EndUpdate();
        }

        public void ClearDisplay()
        {
            _historyManager.ClearHistory();
            RefreshDisplay();
        }
    }
}
