using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public class SplashScreen : Screen
{
    public enum Type
    {
        Intro
    }

    public Type SplashType;
    private Action<SplashScreen, float> _onUpdate;

    public SplashScreen(Engine engine, Type splashType, Action<SplashScreen, float> onUpdate) : base(engine)
    {
        this.SplashType = splashType;
        this._onUpdate = onUpdate;
    }

    public override void Update(float deltaTime)
    {
        this._onUpdate(this, deltaTime);
    }
}
