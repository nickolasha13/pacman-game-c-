using CommonLogic.Core;
using CommonLogic.Game.Screens;
using GameConsole.Extensions;

namespace GameConsole.Screens;

public class SplashRenderer : IScreenRenderer
{
    private const string IntroLogo = @"
#######\                           ##\      ##\                     
##  __##\                          ###\    ### |                    
## |  ## |######\   #######\       ####\  #### | ######\  #######\  
#######  |\____##\ ##  _____|      ##\##\## ## | \____##\ ##  __##\ 
##  ____/ ####### |## /            ## \###  ## | ####### |## |  ## |
## |     ##  __## |## |            ## |\#  /## |##  __## |## |  ## |
## |     \####### |\#######\       ## | \_/ ## |\####### |## |  ## |
\__|      \_______| \_______|      \__|     \__| \_______|\__|  \__|";
    public void Render(Screen screen, Vec2 dimensions, Symbol?[,] buffer)
    {
        string text;
        switch (((SplashScreen)screen).SplashType)
        {
            case SplashScreen.Type.Intro: text = IntroLogo; break;
            default: throw new ArgumentOutOfRangeException();
        }
        var splashLines = text.Substring(1).ReplaceLineEndings().Split('\n');
        var splashWidth = splashLines.Max(l => l.Length);
        var splashHeight = splashLines.Length;
        
        var splashX = (dimensions.X - splashWidth) / 2;
        var splashY = (dimensions.Y - splashHeight) / 2;
        
        for (var y = 0; y < splashHeight; y++)
        {
            for (var x = 0; x < splashWidth; x++)
            {
                if (x >= splashLines[y].Length) continue;
                if (splashY + y >= dimensions.Y || splashX + x >= dimensions.X || splashY + y < 0 || splashX + x < 0) continue;
                buffer[splashY + y, splashX + x] = new Symbol(splashLines[y][x], splashX + x, splashY + y, ConsoleColor.White, ConsoleColor.Black);
            }
        }
    }
}
