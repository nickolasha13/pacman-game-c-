using CommonLogic.Core;
using CommonLogic.Game.Screens;
using SFML.Graphics;
using SFML.System;

namespace GameGui.Screens;

public class DialogRenderer : IScreenRenderer
{
    private const string GameOver = @"
  #####   ###   ##   ## ######
 ##      ## ##  ### ### ##
##      ##   ## ####### ##
##  ### ##   ## ####### #####
##   ## ####### ## # ## ##
 ##  ## ##   ## ##   ## ##
  ##### ##   ## ##   ## ######


  #####  ##   ## ###### ######
 ##   ## ##   ## ##     ##   ##
 ##   ## ##   ## ##     ##   ##
 ##   ## ### ### #####  ##  ###
 ##   ##  #####  ##     #####
 ##   ##   ###   ##     ## ###
  #####     #    ###### ##  ###";

    private const string YouWin = @"
###  ###    ######    ###   ###
###  ###  ###    ###  ###   ###
###  ###  ###    ###  ###   ###
 ######   ###    ###  ###   ###
   ##     ###    ###  ###   ###
   ##     ###    ###  ###   ###
   ##       ######     #######


###      ###  ######  ###   ###
###      ###   ####   ####  ###
###      ###   ####   ##### ###
###  ##  ###   ####   #########
############   ####   ### #####
#####  #####   ####   ###  ####
###      ###  ######  ###   ###";

    public void Render(Screen screen, Vec2 dimensions, RenderWindow window)
    {
        var dialogScreen = (DialogScreen)screen;

        var splashArtText = "";
        switch (dialogScreen.DialogType)
        {
            case DialogScreen.Type.GameOver:
                splashArtText = GameOver;
                break;
            case DialogScreen.Type.YouWin:
                splashArtText = YouWin;
                break;
        }

        var textLines = dialogScreen.Text.ReplaceLineEndings().Split('\n');

        if (textLines.Length == 1 && textLines[0] == "")
            textLines = Array.Empty<string>();

        var totalHeight = textLines.Length + 1;
        var splashY = 0;
        if (splashArtText != "")
        {
            var splashLines = splashArtText.Substring(1).ReplaceLineEndings().Split('\n');
            var splashWidth = splashLines.Max(l => l.Length);
            totalHeight += splashLines.Length + 2;

            var splashX = (dimensions.X / 10 - splashWidth) / 2;
            splashY = (dimensions.Y / 10 - totalHeight) / 2;

            for (var y = 0; y < splashLines.Length; y++)
                window.Draw(new Text(splashLines[y], Resources.Font("PressStart2P"))
                {
                    CharacterSize = 10,
                    Position = new Vector2f(splashX * 10, (splashY + y) * 10),
                    FillColor = Color.White
                });
        }
        else
        {
            totalHeight += 1;
            splashY = (dimensions.Y / 10 - totalHeight) / 2;
        }

        if (textLines.Length > 0)
        {
            var textY = splashY + totalHeight - textLines.Length - 2;
            var textX = (dimensions.X / 10 - textLines.Max(l => l.Length)) / 2;
            for (var i = 0; i < textLines.Length; i++)
                PrintText(textLines[i], textX * 10, (textY + i) * 10, Color.White, null, window);
        }

        var buttonsTotalWidth = dialogScreen.Buttons.Sum(b => b.Text.Length) + dialogScreen.Buttons.Length - 1;

        var buttonsX = (dimensions.X / 10 - buttonsTotalWidth) / 2;
        var buttonsY = splashY + totalHeight - 1;

        for (var i = 0; i < dialogScreen.Buttons.Length; i++)
        {
            var button = dialogScreen.Buttons[i];
            var color = i == dialogScreen.SelectedButtonIndex ? Color.Yellow : new Color(0x808080ff);
            Color? backgroundColor = i == dialogScreen.SelectedButtonIndex ? new Color(0x444444ff) : null;
            PrintText(button.Text, buttonsX, buttonsY, color, backgroundColor, window);
            buttonsX += button.Text.Length + 1;
        }
    }

    private void PrintText(string text, int x, int y, Color color, Color? backgroundColor, RenderWindow window)
    {
        var font = Resources.Font("PressStart2P");
        if (backgroundColor != null)
            window.Draw(new RectangleShape(new Vector2f(text.Length * 10, 10))
            {
                Position = new Vector2f(x * 10, y * 10),
                FillColor = backgroundColor.Value
            });
        window.Draw(new Text(text, font)
        {
            CharacterSize = 10,
            Position = new Vector2f(x * 10, y * 10),
            FillColor = color
        });
    }
}