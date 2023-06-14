using CommonLogic.Core;
using SFML.Graphics;

namespace GameGui.Extensions;

public abstract class RenderExtension<TElement> : CommonLogic.Core.IRenderExtension, IRenderExtension where TElement : Element
{
    public virtual Type[] ElementTypes() => new[] { typeof(TElement) };

    protected abstract Texture? RenderElement(TElement element, EngineGui engine, float deltaTime, int x, int y);

    public Texture? RenderElement(Element element, EngineGui engine, float deltaTime, int x, int y)
    {
        return RenderElement((TElement)element, engine, deltaTime, x, y);
    }
}

public interface IRenderExtension
{
    Texture? RenderElement(Element element, EngineGui engine, float deltaTime, int x, int y);
}
