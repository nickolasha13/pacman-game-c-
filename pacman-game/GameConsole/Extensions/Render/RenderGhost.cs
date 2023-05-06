using CommonLogic.Game.Elements.Entity;
using CommonLogic.Game.Elements.Entity.Ghosts;

namespace GameConsole.Extensions.Render;

public class RenderGhost: RenderExtension<Ghost>
{
    public override Type[] ElementTypes() => new [] { typeof(Blinky), typeof(Pinky), typeof(Inky), typeof(Clyde) };

    protected override Symbol[] RenderElement(Ghost element, EngineConsole engine, float deltaTime, int x, int y)
    {
        byte color = 255;
        if (element is Blinky) color = 9;
        if (element is Pinky) color = 13;
        if (element is Inky) color = 14;
        if (element is Clyde) color = 11;
        char symbol = engine.World!.IsGhostsFrightened ? 'V' : 'A';
        return new [] { new Symbol(symbol, x, y, color, 0) };
    }
}
