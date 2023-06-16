using CommonLogic.Core;
using GameConsole.Extensions;

namespace GameConsole.Screens;

public interface IScreenRenderer
{
    public void Render(Screen screen, Vec2 dimensions, Symbol?[,] buffer);
}