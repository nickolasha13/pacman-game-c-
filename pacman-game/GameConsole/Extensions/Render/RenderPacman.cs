using CommonLogic.Game.Elements.Entity;

namespace GameConsole.Extensions.Render;

public class RenderPacman: RenderExtension<Pacman>
{
    protected override Symbol[] RenderElement(Pacman element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new [] { new Symbol('C', x, y, ConsoleColor.Black, ConsoleColor.Yellow) };
    }
}
