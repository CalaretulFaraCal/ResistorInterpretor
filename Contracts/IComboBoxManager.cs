namespace ResistorInterpretor.Contracts;

public interface IComboBoxManager
{
    ComboBox ComboBox { get; }
    string GetSelectedColor(string propertyType, string defaultColor);
    void PopulateComboBox(string propertyType);
    void RestoreComboBoxSelection(string previousSelection);
    void UpdateComboBoxVisibility(int bandCount, int previousBandCount, string propertyType);
    int GetItemCount();
    object GetItemAt(int index);
    void SetSelectedIndex(int index);
}