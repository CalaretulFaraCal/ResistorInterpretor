namespace ResistorInterpretor.Contracts
{
    public interface ILabelManager
    {
        void SetVisibility(bool visible);
        void UpdateLabelVisibility(string name, int bandCount);
    }
}