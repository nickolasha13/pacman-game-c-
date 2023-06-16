using CommonLogic.Game.Elements.Map;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderFloor : RenderExtension<Floor>
{
    protected override Texture? RenderElement(Floor element, EngineGui engine, float deltaTime, int x, int y)
    {
        return null;
    }
}