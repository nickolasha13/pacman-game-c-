using CommonLogic.Game.Elements.Entity;
using CommonLogic.Game.Elements.Entity.Ghosts;

namespace GameConsole.Extensions.Render;

public class RenderGhost: RenderExtension<Ghost>
{
    public override Type[] ElementTypes() => new [] { typeof(Blinky), typeof(Pinky), typeof(Inky), typeof(Clyde) };

    protected override Symbol[] RenderElement(Ghost element, EngineConsole engine, float deltaTime, int x, int y)
    {
        var color = ConsoleColor.White;
        if (element is Blinky) color = ConsoleColor.Red;
        if (element is Pinky) color = ConsoleColor.Magenta;
        if (element is Inky) color = ConsoleColor.Cyan;
        if (element is Clyde) color = ConsoleColor.Yellow;
        char symbol = engine.World!.IsGhostsFrightened ? 'V' : 'A';
        return new [] { new Symbol(symbol, x, y, color, ConsoleColor.Black) };
    }
}
