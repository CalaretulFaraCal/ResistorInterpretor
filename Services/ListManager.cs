using ResistorInterpretor.Contracts;

namespace ResistorInterpretor.Services;

public class ListManager(ListView listView): IListManager
{
    public void Initialize()
    {
        listView.View = View.Details;
        listView.Columns.Clear();
        listView.Columns.Add("Band", 160);
        listView.HeaderStyle = ColumnHeaderStyle.None;
        listView.FullRowSelect = false;
        listView.GridLines = false;
    }

    public void Clear()
    {
        listView.Items.Clear();
    }

    public void AddBand(Color color, string? text = null)
    {
        var item = new ListViewItem(text ?? color.Name)
        {
            BackColor = color,
            ForeColor = UI.GetTextColor(color),
        };
        listView.Items.Add(item);
    }
}