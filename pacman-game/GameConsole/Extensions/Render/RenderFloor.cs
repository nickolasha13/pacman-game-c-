using CommonLogic.Game.Elements.Map;

namespace GameConsole.Extensions.Render;

public class RenderFloor: RenderExtension<Floor>
{
    protected override Symbol[] RenderElement(Floor element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new [] { new Symbol(' ', x, y, null, 0) };
    }
}
