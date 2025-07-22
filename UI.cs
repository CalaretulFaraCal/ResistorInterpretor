namespace ResistorInterpretor;

public static class UI
{
    public static void UpdateLabelVisibility(Label label1, Label label2, int bandCount)
    {
        label1.Visible = bandCount > 3;
        label2.Visible = bandCount == 6;
    }

    public static Color GetTextColor(Color backgroundColor)
    {
        var brightness = (int)Math.Sqrt(backgroundColor.R * backgroundColor.R * 0.241 + backgroundColor.G * backgroundColor.G * 0.691 + backgroundColor.B * backgroundColor.B * 0.068);

        return brightness < 130 ? Color.White : Color.Black;
    }

    public static void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}