using CommonLogic.Game.Elements.Entity;
using CommonLogic.Game.Elements.Entity.Ghosts;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderGhost : RenderExtension<Ghost>
{
    public override Type[] ElementTypes()
    {
        return new[] { typeof(Blinky), typeof(Pinky), typeof(Inky), typeof(Clyde) };
    }

    protected override Texture? RenderElement(Ghost element, EngineGui engine, float deltaTime, int x, int y)
    {
        Texture? t = null;
        if (element is Blinky)
            t = Resources.Texture("blinky-right-1");
        if (element is Pinky)
            t = Resources.Texture("pinky-right-1");
        if (element is Inky)
            t = Resources.Texture("inky-right-1");
        if (element is Clyde)
            t = Resources.Texture("clyde-right-1");
        if (engine.World!.IsGhostsFrightened)
            t = Resources.Texture("frightened-1");
        return t;
    }
}