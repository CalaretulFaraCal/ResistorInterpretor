namespace ResistorInterpretor.Contracts;

public interface IMainFormUI
{
    string Value { get; }
    string Suffix { get; }
    void SetResistanceValue(double value);
}