using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public class SplashScreen : Screen
{
    public enum Type
    {
        Intro
    }

    private readonly Action<SplashScreen, float> _onUpdate;

    public Type SplashType;

    public SplashScreen(Engine engine, Type splashType, Action<SplashScreen, float> onUpdate) : base(engine)
    {
        SplashType = splashType;
        _onUpdate = onUpdate;
    }

    public override void Update(float deltaTime)
    {
        _onUpdate(this, deltaTime);
    }
}