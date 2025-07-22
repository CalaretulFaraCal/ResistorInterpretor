namespace ResistorInterpretor.Contracts;

public interface IListManager
{
    void Initialize();
    void Clear();
    void AddBand(Color color, string? text = null);
}