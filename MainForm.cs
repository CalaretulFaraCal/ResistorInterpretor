using ResistorInterpretor.Contracts;
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

    private readonly IRadioButtonManager radioButtonManager;

    private readonly ValueConverterLogic logic;

    public string Value => inputValue.Text;
    public string Suffix => comboBoxUnits.SelectedItem?.ToString() ?? "Ohm";

    public MainForm()
    {
        InitializeComponent();

        listManager = new ListManager(listViewVTC);

        comboBoxManageTolerance = new ComboBoxManager(comboBoxTolerance);
        comboBoxManageTempCoeff = new ComboBoxManager(comboBoxTempCoeff);
        comboBoxManageUnits = new ComboBoxManager(comboBoxUnits);
        comboBoxManageBands = new ComboBoxManager(comboBoxBands);
        logic = new ValueConverterLogic(this, listManager, comboBoxManageTolerance, comboBoxManageTempCoeff);


        radioButtonManager = new RadioButtonManager(
            radioButtonBands_3,
            radioButtonBands_4,
            radioButtonBands_5,
            radioButtonBands_6,
            logic,
            comboBoxManageTolerance,
            comboBoxManageTempCoeff);

        labelToleranceVTC.Visible = false;
        labelTempCoeffVTC.Visible = false;

        comboBoxManageBands.PopulateComboBox("bands");
        comboBoxBands.SelectedIndex = logic.previousBandCount - 3;

        listManager.Initialize();
        comboBoxManageUnits.PopulateUnitComboBox();

        comboBoxManageTolerance.UpdateComboBoxVisibility(logic.previousBandCount, -1, "tolerance");
        comboBoxManageTempCoeff.UpdateComboBoxVisibility(logic.previousBandCount, -1, "temperatureCoefficient");

        foreach (var rb in new[] {
            radioButtonBands_3,
            radioButtonBands_4,
            radioButtonBands_5,
            radioButtonBands_6 })
            rb.CheckedChanged += (_, _) =>
            {
                var bandCount = radioButtonManager.UpdateBandCount();
                UI.UpdateLabelVisibility(labelToleranceVTC, labelTempCoeffVTC, bandCount);
            };
    }

    private void button1_Click(object sender, EventArgs e)
    {
        logic.Convert();
    }
}