using CommonLogic.Core;
using CommonLogic.Game.Screens;
using GameConsole.Extensions;

namespace GameConsole.Screens;

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

    public void Render(Screen screen, Vec2 dimensions, Symbol?[,] buffer)
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

            var splashX = (dimensions.X - splashWidth) / 2;
            splashY = (dimensions.Y - totalHeight) / 2;

            for (var y = 0; y < splashLines.Length; y++)
            for (var x = 0; x < splashWidth; x++)
            {
                if (x >= splashLines[y].Length)
                    continue;
                if (splashY + y >= dimensions.Y || splashX + x >= dimensions.X || splashY + y < 0 || splashX + x < 0)
                    continue;
                buffer[splashY + y, splashX + x] = new Symbol(splashLines[y][x], splashX + x, splashY + y, 255, 0);
            }
        }
        else
        {
            totalHeight += 1;
            splashY = (dimensions.Y - totalHeight) / 2;
        }

        if (textLines.Length > 0)
        {
            var textY = splashY + totalHeight - textLines.Length - 2;
            var textX = (dimensions.X - textLines.Max(l => l.Length)) / 2;
            for (var i = 0; i < textLines.Length; i++)
                PrintText(textLines[i], textX, textY + i, 255, 0, buffer);
        }

        var buttonsTotalWidth = dialogScreen.Buttons.Sum(b => b.Text.Length) + dialogScreen.Buttons.Length - 1;

        var buttonsX = (dimensions.X - buttonsTotalWidth) / 2;
        var buttonsY = splashY + totalHeight - 1;

        for (var i = 0; i < dialogScreen.Buttons.Length; i++)
        {
            var button = dialogScreen.Buttons[i];
            var color = i == dialogScreen.SelectedButtonIndex ? (byte)11 : (byte)8;
            var backgroundColor = i == dialogScreen.SelectedButtonIndex ? (byte)238 : (byte)0;
            PrintText(button.Text, buttonsX, buttonsY, color, backgroundColor, buffer);
            buttonsX += button.Text.Length + 1;
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