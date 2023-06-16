using CommonLogic.Game.Elements.Entity;

namespace GameConsole.Extensions.Render;

public class RenderEnergizer : RenderExtension<Energizer>
{
    protected override Symbol[] RenderElement(Energizer element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new[] { new Symbol('+', x, y, 3, 0) };
    }
}