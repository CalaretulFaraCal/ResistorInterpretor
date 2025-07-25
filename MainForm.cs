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
    private readonly IComboBoxManager[] colorBandManagers;

    private readonly ILabelManager toleranceLabelManager;
    private readonly ILabelManager tempCoeffLabelManager;

    private readonly IRadioButtonManager radioButtonManager;

    private readonly ValueConverterLogic logic;
    private readonly ColorConverterLogic colorLogic;


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

        toleranceLabelManager = new LabelManager(Tolerance);
        tempCoeffLabelManager = new LabelManager(TemperatureCoefficient);

        logic = new ValueConverterLogic(this, listManager, comboBoxManageTolerance, comboBoxManageTempCoeff);
        colorLogic = new ColorConverterLogic(comboBoxBands, colorBandManagers, labelResult);

        radioButtonManager = new RadioButtonManager(
            radioButtonBands_3,
            radioButtonBands_4,
            radioButtonBands_5,
            radioButtonBands_6,
            logic);

        toleranceLabelManager.SetVisibility(false);
        tempCoeffLabelManager.SetVisibility(false);

        comboBoxManageBands.PopulateComboBox("bands");
        comboBoxBands.SelectedIndex = logic.previousBandCount - 3;

        for (int i = 0; i < colorBandManagers.Length; i++)
        {
            colorBandManagers[i].PopulateComboBox(bandComboKeys[i]);
        }


        comboBoxManageTolerance.UpdateComboBoxVisibility(logic.previousBandCount, -1, "tolerance");
        comboBoxManageTempCoeff.UpdateComboBoxVisibility(logic.previousBandCount, -1, "temperatureCoefficient");
        colorLogic.UpdateBandComboBoxVisibility(logic.previousBandCount, -1, colorBandManagers);

        comboBoxManageUnits.PopulateComboBox("units");

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

            colorLogic.UpdateBandComboBoxVisibility(bandCount, logic.previousBandCount, colorBandManagers); logic.previousBandCount = bandCount;
        };

    }

    private void button2_Click(object sender, EventArgs e)
    {
        colorLogic.Convert();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        logic.Convert();
    }
}