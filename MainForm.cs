using ResistorInterpretor.Contracts;
using ResistorInterpretor.History;
using ResistorInterpretor.Logic;
using ResistorInterpretor.Services;

namespace ResistorInterpretor;

public partial class MainForm : Form, IMainFormUI
{
    private readonly ListManager listManager;

    private readonly IComboBoxManager comboBoxManageTolerance;
    private readonly IComboBoxManager comboBoxManageTempCoeff;
    private readonly IComboBoxManager comboBoxManageUnits;
    private readonly IComboBoxManager comboBoxManageBands;
    private readonly IComboBoxManager comboBoxSorting;
    private readonly IComboBoxManager comboBoxFiltering;
    private readonly IComboBoxManager[] colorBandManagers;

    private readonly ILabelManager toleranceLabelManager;
    private readonly ILabelManager tempCoeffLabelManager;
    private readonly ILabelManager[] colorBandLabelManagers;

    private readonly IRadioButtonManager radioButtonManager;

    private readonly ValueConverterLogic logic;
    private readonly ColorConverterLogic colorLogic;

    private readonly IValueToColorHistoryManager valueToColorHistoryManager;
    private readonly IValueToColorHistoryDisplay valueToColorHistoryDisplay;
    private readonly IHistoryRestoreManager historyRestoreManager;
    private readonly IColorToValueHistoryManager colorToValueHistoryManager;
    private readonly IColorToValueHistoryDisplay colorToValueHistoryDisplay;
    private readonly ISortManager sortManager;
    private readonly IFilterManager filterManager;

    private readonly IGenerateBandsManager generateBandsManager;
    private readonly IClearHistoryManager clearHistoryManager;

    public string Value => inputValue.Text;
    public string Suffix => comboBoxUnits.SelectedItem?.ToString() ?? "Ohm";

    public MainForm()
    {
        InitializeComponent();

        listManager = new ListManager(listViewVTC);

        generateBandsManager = new GenerateBandsManager();

        comboBoxManageTolerance = new ComboBoxManager(comboBoxTolerance);
        comboBoxManageTempCoeff = new ComboBoxManager(comboBoxTempCoeff);
        comboBoxManageUnits = new ComboBoxManager(comboBoxUnits);
        comboBoxManageBands = new ComboBoxManager(comboBoxBands);
        comboBoxSorting = new ComboBoxManager(comboBoxSort);
        comboBoxSorting = new ComboBoxManager(comboBoxSortCTV);
        comboBoxFiltering = new ComboBoxManager(comboBoxFilter);
        comboBoxFiltering = new ComboBoxManager(comboBoxFilterCTV);

        colorBandManagers = new IComboBoxManager[]
        {
            new ComboBoxManager(comboBox1),
            new ComboBoxManager(comboBox2),
            new ComboBoxManager(comboBox3),
            new ComboBoxManager(comboBox4),
            new ComboBoxManager(comboBox5),
            new ComboBoxManager(comboBox6)
        };
        var bandComboKeys = new[]
        {
            "comboBox1", "comboBox2", "comboBox3",
            "comboBox4", "comboBox5", "comboBox6"
        };

        colorBandLabelManagers = new ILabelManager[]
        {
            new LabelManager(label1),
            new LabelManager(label2),
            new LabelManager(label3),
            new LabelManager(label4),
            new LabelManager(label5),
            new LabelManager(label6)
        };

        toleranceLabelManager = new LabelManager(Tolerance);
        tempCoeffLabelManager = new LabelManager(TemperatureCoefficient);

        logic = new ValueConverterLogic(this, listManager, comboBoxManageTolerance, comboBoxManageTempCoeff, generateBandsManager);
        colorLogic = new ColorConverterLogic(comboBoxBands, colorBandManagers, labelResult, colorBandLabelManagers);

        radioButtonManager = new RadioButtonManager(
            radioButtonBands_3,
            radioButtonBands_4,
            radioButtonBands_5,
            radioButtonBands_6,
            logic);

        historyRestoreManager = new HistoryRestoreManager(
            this,
            comboBoxManageUnits,
            comboBoxManageBands,
            comboBoxManageTolerance,
            comboBoxManageTempCoeff,
            colorBandManagers,
            logic,
            radioButtonManager,
            colorLogic);

        valueToColorHistoryManager = new ValueToColorHistoryManager();

        valueToColorHistoryDisplay = new ValueToColorHistoryDisplay(
            listView1,
            valueToColorHistoryManager,
            historyRestoreManager);

        colorToValueHistoryManager = new ColorToValueHistoryManager();
        colorToValueHistoryDisplay = new ColorToValueHistoryDisplay(
            listView2,
            colorToValueHistoryManager,
            historyRestoreManager,
            generateBandsManager
        );

        clearHistoryManager = new ClearHistoryManager(
            valueToColorHistoryManager,
            colorToValueHistoryManager,
            valueToColorHistoryDisplay,
            colorToValueHistoryDisplay);

        sortManager = new SortManager();
        filterManager = new FilterManager();

        toleranceLabelManager.SetVisibility(false);
        tempCoeffLabelManager.SetVisibility(false);

        comboBoxManageUnits.PopulateComboBox("units");
        comboBoxSorting.PopulateComboBox("sortVTC");
        comboBoxSorting.PopulateComboBox("sortCTV");
        comboBoxFiltering.PopulateComboBox("filterVTC");
        comboBoxFiltering.PopulateComboBox("filterCTV");

        comboBoxManageBands.PopulateComboBox("bands");
        comboBoxBands.SelectedIndex = logic.previousBandCount - 3;

        for (int i = 0; i < colorBandManagers.Length; i++)
            colorBandManagers[i].PopulateComboBox(bandComboKeys[i]);

        comboBoxManageTolerance.UpdateComboBoxVisibility(logic.previousBandCount, -1, "tolerance");
        comboBoxManageTempCoeff.UpdateComboBoxVisibility(logic.previousBandCount, -1, "temperatureCoefficient");
        colorLogic.UpdateBandComboBoxVisibility(logic.previousBandCount, -1, colorBandManagers);

        listManager.Initialize();

        radioButtonManager.BandCountChanged += (_, _) =>
        {
            var bandCount = radioButtonManager.UpdateBandCount();
            toleranceLabelManager.UpdateLabelVisibility("tolerance", bandCount);
            tempCoeffLabelManager.UpdateLabelVisibility("temperatureCoefficient", bandCount);
        };

        comboBoxBands.SelectedIndexChanged += (_, _) =>
        {
            int bandCount = comboBoxBands.SelectedIndex + 3;

            comboBoxManageTolerance.UpdateComboBoxVisibility(bandCount, logic.previousBandCount, "tolerance");
            comboBoxManageTempCoeff.UpdateComboBoxVisibility(bandCount, logic.previousBandCount, "temperatureCoefficient");

            colorLogic.UpdateBandComboBoxVisibility(bandCount, logic.previousBandCount, colorBandManagers);
            logic.previousBandCount = bandCount;
        };

        comboBoxSorting.ComboBox.SelectedIndexChanged += (_, _) =>
        {
            ApplyFilterAndSort();
        };

        comboBoxFiltering.ComboBox.SelectedIndexChanged += (s, e) =>
        {
            filterManager.CurrentFilterValue = comboBoxFilter.SelectedItem?.ToString() ?? "None";
            ApplyFilterAndSort();
        };

        comboBoxSortCTV.SelectedIndexChanged += (_, _) =>
        {
            ApplyFilterAndSortColorToValue();
        };

        comboBoxFilterCTV.SelectedIndexChanged += (s, e) =>
        {
            filterManager.CurrentFilterValue = comboBoxFilterCTV.SelectedItem?.ToString() ?? "None";
            ApplyFilterAndSortColorToValue();
        };

        logic.HistoryEntry += (sender, args) =>
        {
            valueToColorHistoryManager.SaveEntry(
                args.Value,
                args.Unit,
                args.BandCount,
                args.ToleranceColor,
                args.TempCoeffColor);
            ApplyFilterAndSort();
        };

        colorLogic.ConversionCompleted += (sender, args) =>
        {
            colorToValueHistoryManager.SaveEntry(args.BandCount, args.ColorBandNames);
            colorToValueHistoryDisplay.RefreshDisplay();
        };
    }

    public void SetResistanceValue(double value)
    {
        inputValue.Text = value.ToString();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        colorLogic.Convert();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        logic.Convert();
    }

    private void buttonClearHistory_Click(object sender, EventArgs e)
    {
        clearHistoryManager.ClearHistoryVTC();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        clearHistoryManager.ClearHistoryCTV();
    }

    private void ApplyFilterAndSort()
    {
        var allEntries = valueToColorHistoryManager.GetAllEntries();
        var filtered = filterManager.ApplyFilter(allEntries);
        var sortKey = comboBoxSorting.ComboBox.SelectedItem?.ToString() ?? "All";
        var sorted = sortKey == "All" ? filtered : sortManager.SortVTC(filtered, sortKey);
        valueToColorHistoryDisplay.RefreshDisplay(sorted);
    }

    private void ApplyFilterAndSortColorToValue()
    {
        var allEntries = colorToValueHistoryManager.GetAllEntries();
        var filtered = filterManager.ApplyFilter(allEntries);
        var sortKey = comboBoxSortCTV.SelectedItem?.ToString() ?? "All";
        var sorted = sortKey == "All" ? filtered : sortManager.SortCTV(filtered, sortKey);
        colorToValueHistoryDisplay.RefreshDisplay(sorted);
    }
}