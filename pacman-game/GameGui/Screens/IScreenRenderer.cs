using CommonLogic.Core;
using SFML.Graphics;

namespace GameGui.Screens;

public interface IScreenRenderer
{
    public void Render(Screen screen, Vec2 dimensions, RenderWindow window);
}