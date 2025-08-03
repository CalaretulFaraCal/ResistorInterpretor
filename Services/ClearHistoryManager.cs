using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;

namespace ResistorInterpretor.Services
{
    public class ClearHistoryManager : IClearHistoryManager
    {
        private readonly IValueToColorHistoryManager historyManager;
        private readonly IValueToColorHistoryDisplay historyDisplay;

        public ClearHistoryManager(IValueToColorHistoryManager historyManager, IValueToColorHistoryDisplay historyDisplay)
        {
            this.historyManager = historyManager;
            this.historyDisplay = historyDisplay;
        }

        public void ClearHistory()
        {
            historyManager.ClearHistory();
            historyDisplay.RefreshDisplay();
        }
    }
}