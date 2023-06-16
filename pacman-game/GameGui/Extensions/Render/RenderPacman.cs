using CommonLogic.Game.Elements.Entity;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderPacman : RenderExtension<Pacman>
{
    protected override Texture RenderElement(Pacman element, EngineGui engine, float deltaTime, int x, int y)
    {
        return Resources.Texture("mspac-right-1");
    }
}