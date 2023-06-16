using CommonLogic.Game.Elements.Entity;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderCoin : RenderExtension<Coin>
{
    protected override Texture RenderElement(Coin element, EngineGui engine, float deltaTime, int x, int y)
    {
        return Resources.Texture("coin");
    }
}