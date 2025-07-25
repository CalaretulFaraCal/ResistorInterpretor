namespace ResistorInterpretor;

public static class UI
{
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