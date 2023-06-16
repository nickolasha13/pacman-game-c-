using CommonLogic.Core;
using CommonLogic.Game.Screens;
using SFML.Graphics;
using SFML.System;

namespace GameGui.Screens;

public class MenuRenderer : IScreenRenderer
{
    public void Render(Screen screen, Vec2 dimensions, RenderWindow window)
    {
        var menuScreen = (MenuScreen)screen;

        var y = 1;

        PrintText(menuScreen.Title!, 1, y, Color.Cyan, null, window);

        y += 2;

        for (var i = 0; i < menuScreen.Entries!.Length; i++)
        {
            var entry = menuScreen.Entries[i];
            var color = i == menuScreen.SelectedEntryIndex ? Color.Yellow : new Color(0xa8a8a8ff);
            Color? backgroundColor = i == menuScreen.SelectedEntryIndex ? new Color(0x444444ff) : null;
            PrintText(entry.Text, 1, y, color, backgroundColor, window);
            if (entry.SubText != null)
                PrintText(entry.SubText, 2 + entry.Text.Length, y, new Color(0x444444ff), null, window);
            y++;
        }

        y += 1;

        var description = menuScreen.Entries[menuScreen.SelectedEntryIndex].Description;
        if (description != null)
        {
            PrintText(description, 1, y, Color.Cyan, null, window);
            y += 2;
        }
    }

    private void PrintText(string text, int x, int y, Color color, Color? backgroundColor, RenderWindow window)
    {
        var font = Resources.Font("PressStart2P");
        if (backgroundColor != null)
            window.Draw(new RectangleShape(new Vector2f(text.Length * 12, 12))
            {
                Position = new Vector2f(x * 12, y * 12),
                FillColor = backgroundColor.Value
            });
        window.Draw(new Text(text, font)
        {
            CharacterSize = 12,
            Position = new Vector2f(x * 12, y * 12),
            FillColor = color
        });
    }
}