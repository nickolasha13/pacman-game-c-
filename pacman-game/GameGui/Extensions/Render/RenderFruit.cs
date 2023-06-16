using CommonLogic.Game.Elements.Entity;
using SFML.Graphics;

namespace GameGui.Extensions.Render;

public class RenderFruit : RenderExtension<Fruit>
{
    protected override Texture RenderElement(Fruit element, EngineGui engine, float deltaTime, int x, int y)
    {
        return Resources.Texture("fruit" + element.FruitType);
    }
}