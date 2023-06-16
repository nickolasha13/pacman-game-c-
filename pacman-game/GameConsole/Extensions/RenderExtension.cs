using CommonLogic.Core;

namespace GameConsole.Extensions;

public abstract class RenderExtension<TElement> : CommonLogic.Core.IRenderExtension, IRenderExtension
    where TElement : Element
{
    public virtual Type[] ElementTypes()
    {
        return new[] { typeof(TElement) };
    }

    public Symbol[] RenderElement(Element element, EngineConsole engine, float deltaTime, int x, int y)
    {
        return RenderElement((TElement)element, engine, deltaTime, x, y);
    }

    protected abstract Symbol[] RenderElement(TElement element, EngineConsole engine, float deltaTime, int x, int y);
}

public struct Symbol
{
    public char Character;
    public byte? Foreground;
    public byte? Background;
    public int X;
    public int Y;

    public Symbol(char character, int x, int y, byte? foreground = null, byte? background = null)
    {
        Character = character;
        Foreground = foreground;
        Background = background;
        X = x;
        Y = y;
    }
}

public interface IRenderExtension
{
    Symbol[] RenderElement(Element element, EngineConsole engine, float deltaTime, int x, int y);
}