using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services
{
    public class ClearHistoryManager : IClearHistoryManager
    {
        private readonly IValueToColorHistoryManager historyManagerVTC;
        private readonly IColorToValueHistoryManager historyManagerCTV;
        private readonly IValueToColorHistoryDisplay historyDisplayVTC;
        private readonly IColorToValueHistoryDisplay historyDisplayCTV;

        public ClearHistoryManager(
            IValueToColorHistoryManager historyManagerVTC,
            IColorToValueHistoryManager historyManagerCTV,
            IValueToColorHistoryDisplay historyDisplayVTC,
            IColorToValueHistoryDisplay historyDisplayCTV)
        {
            this.historyManagerVTC = historyManagerVTC;
            this.historyManagerCTV = historyManagerCTV;
            this.historyDisplayVTC = historyDisplayVTC;
            this.historyDisplayCTV = historyDisplayCTV;
        }

        public void ClearHistoryVTC()
        {
            historyManagerVTC.ClearHistory();
            historyDisplayVTC.RefreshDisplay();
        }

        public void ClearHistoryCTV()
        {
            historyManagerCTV.ClearHistory();
            historyDisplayCTV.RefreshDisplay();
        }
    }
}