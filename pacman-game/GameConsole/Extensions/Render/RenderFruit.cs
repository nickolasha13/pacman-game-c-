using CommonLogic.Game.Elements.Entity;

namespace GameConsole.Extensions.Render;

public class RenderFruit : RenderExtension<Fruit>
{
    protected override Symbol[] RenderElement(Fruit element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new[] { new Symbol('%', x, y, 160, 0) };
    }
}