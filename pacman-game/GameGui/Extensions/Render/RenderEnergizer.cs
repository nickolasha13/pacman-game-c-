using CommonLogic.Game.Elements.Entity;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderEnergizer: RenderExtension<Energizer>
{
    protected override Texture RenderElement(Energizer element, EngineGui engine, float deltaTime, int x, int y)
    {
        return Resources.Texture("energizer");
    }
}
