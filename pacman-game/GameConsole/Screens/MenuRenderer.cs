using CommonLogic.Core;
using CommonLogic.Game.Screens;
using GameConsole.Extensions;

namespace GameConsole.Screens;

public class MenuRenderer : IScreenRenderer
{
    public void Render(Screen screen, Vec2 dimensions, Symbol?[,] buffer)
    {
        var menuScreen = (MenuScreen)screen;

        var y = 1;

        PrintText(menuScreen.Title!, 1, y, 14, 0, buffer);

        y += 2;

        for (var i = 0; i < menuScreen.Entries!.Length; i++)
        {
            var entry = menuScreen.Entries[i];
            var color = i == menuScreen.SelectedEntryIndex ? (byte)11 : (byte)248;
            var backgroundColor = i == menuScreen.SelectedEntryIndex ? (byte)238 : (byte)0;
            PrintText(entry.Text, 1, y, color, backgroundColor, buffer);
            if (entry.SubText != null)
                PrintText(entry.SubText, 2 + entry.Text.Length, y, 238, 0, buffer);
            y++;
        }

        y += 1;

        var description = menuScreen.Entries[menuScreen.SelectedEntryIndex].Description;
        if (description != null)
        {
            PrintText(description, 1, y, 14, 0, buffer);
            y += 2;
        }
    }

    private void PrintText(string text, int x, int y, byte color, byte backgroundColor, Symbol?[,] buffer)
    {
        for (var i = 0; i < text.Length; i++)
        {
            if (x + i >= buffer.GetLength(1) || y >= buffer.GetLength(0) || x + i < 0 || y < 0)
                continue;
            buffer[y, x + i] = new Symbol(text[i], x + i, y, color, backgroundColor);
        }
    }
}