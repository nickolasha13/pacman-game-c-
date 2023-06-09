using CommonLogic.Core;
using CommonLogic.Game.Screens;
using SFML.Graphics;
using SFML.System;

namespace GameGui.Screens;

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
    public void Render(Screen screen, Vec2 dimensions, RenderWindow window)
    {
        var font = Resources.Font("PressStart2P");
        string text;
        switch (((SplashScreen)screen).SplashType)
        {
            case SplashScreen.Type.Intro: text = IntroLogo; break;
            default: throw new ArgumentOutOfRangeException();
        }
        var splashLines = text.Substring(1).ReplaceLineEndings().Split('\n');
        var splashWidth = splashLines.Max(l => l.Length);
        var splashHeight = splashLines.Length;
        
        var splashX = (dimensions.X / 10 - splashWidth) / 2;
        var splashY = (dimensions.Y / 10 - splashHeight) / 2;
        
        for (var y = 0; y < splashHeight; y++)
        {
            window.Draw(new Text(splashLines[y], font)
            {
                CharacterSize = 10,
                Position = new Vector2f(splashX * 10, splashY * 10 + y * 10)
            });
        }
    }
}
