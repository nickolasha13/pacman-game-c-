using CommonLogic.Game.Elements.Map;

namespace GameConsole.Extensions.Render;

public class RenderWall: RenderExtension<Wall>
{
    protected override Symbol[] RenderElement(Wall element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return new [] { new Symbol('#', x, y, 12, 0) };
    }
}
