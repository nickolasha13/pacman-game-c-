using CommonLogic.Game.Elements.Map;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderWall: RenderExtension<Wall>
{
    protected override Texture RenderElement(Wall element, EngineGui engine, float deltaTime, int x, int y)
    {
        return Resources.Texture("wall");
    }
}
