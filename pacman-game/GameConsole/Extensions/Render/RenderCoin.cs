using CommonLogic.Game.Elements.Entity;

namespace GameConsole.Extensions.Render;

public class RenderCoin: RenderExtension<Coin>
{
    protected override Symbol[] RenderElement(Coin element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new [] { new Symbol('·', x, y, 3, 0) };
    }
}
