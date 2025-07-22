using ResistorInterpretor.Contracts;
using ResistorInterpretor.Services;

namespace ResistorInterpretor;

public partial class Form_ : Form, IMainFormUI
{
    private readonly ListManager listManager;
    private readonly IComboBoxManager comboBoxManageTolerance;
    private readonly IComboBoxManager comboBoxManageTempCoeff;
    private readonly IComboBoxManager comboBoxManageUnits;
    private readonly IComboBoxManager comboBoxManageBands;

    private readonly ConverterLogic logic;

    public string Value => inputValue.Text;
    public string Suffix => comboBoxUnits.SelectedItem?.ToString() ?? "Ohm";

    public Form_()
    {
        InitializeComponent();

        listManager = new ListManager(listViewVTC);

        comboBoxManageTolerance = new ComboBoxManager(comboBoxTolerance);
        comboBoxManageTempCoeff = new ComboBoxManager(comboBoxTempCoeff);
        comboBoxManageUnits = new ComboBoxManager(comboBoxUnits);
        comboBoxManageBands = new ComboBoxManager(comboBoxBands);

        logic = new ConverterLogic(this, listManager, comboBoxManageTolerance, comboBoxManageTempCoeff);

        radioButtonBands_3.Checked = true;
        labelToleranceVTC.Visible = false;
        labelTempCoeffVTC.Visible = false;

        comboBoxManageBands.PopulateComboBox("bands");
        comboBoxBands.SelectedIndex = logic.PreviousBandCount - 3;

        foreach (var rb in new[] { radioButtonBands_3, radioButtonBands_4, radioButtonBands_5, radioButtonBands_6 })
            rb.CheckedChanged += (_, _) => UpdateBandCount();

        listManager.Initialize();
        comboBoxManageUnits.PopulateUnitComboBox();

        comboBoxManageTolerance.UpdateComboBoxVisibility(logic.PreviousBandCount, -1, "tolerance");
        comboBoxManageTempCoeff.UpdateComboBoxVisibility(logic.PreviousBandCount, -1, "temperatureCoefficient");
    }

    private void UpdateBandCount()
    {
        var bandCount = radioButtonBands_4.Checked ? 4 :
            radioButtonBands_5.Checked ? 5 :
            radioButtonBands_6.Checked ? 6 : 3;

        if (bandCount == logic.PreviousBandCount)
            return;

        comboBoxManageTolerance.UpdateComboBoxVisibility(bandCount, logic.PreviousBandCount, "tolerance");
        comboBoxManageTempCoeff.UpdateComboBoxVisibility(bandCount, logic.PreviousBandCount, "temperatureCoefficient");

        UI.UpdateLabelVisibility(labelToleranceVTC, labelTempCoeffVTC, bandCount);
        logic.PreviousBandCount = bandCount;

        if (comboBoxTolerance.Visible)
            comboBoxTolerance.SelectedIndex = 0;
        else
        {   
            comboBoxTolerance.Items.Clear();
            comboBoxTolerance.Visible = false;
        }

        if (comboBoxTempCoeff.Visible)
            comboBoxTempCoeff.SelectedIndex = 0;
        else
        {
            comboBoxTempCoeff.Items.Clear();
            comboBoxTempCoeff.Visible = false;
        }
    }


    private void button1_Click(object sender, EventArgs e)
    {
        logic.Convert();
    }
}