namespace ResistorInterpretor.Contracts;

public interface IComboBoxManager
{
    string GetSelectedColor(string propertyType, string defaultColor);
    void PopulateComboBox(string propertyType);
    void RestoreComboBoxSelection(string previousSelection);
    void UpdateComboBoxVisibility(int bandCount, int previousBandCount, string propertyType);

}