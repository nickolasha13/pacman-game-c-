using CommonLogic.Core.Menus;
using CommonLogic.Game.Screens;

namespace CommonLogic.Core;

public abstract class Engine
{
    public AudioSystem AudioSystem = new();
    public Dictionary<Type, IRenderExtension> Extensions = new();
    public InputProvider Input;
    public List<Screen> Screens = new();
    public GameWorld? World;

    protected Engine(InputProvider input)
    {
        Input = input;
        ShowTimedSplashScreen(SplashScreen.Type.Intro, 1, ExitToMainMenu);
    }

    protected void RegisterExtension(IRenderExtension extension)
    {
        foreach (var type in extension.ElementTypes())
            Extensions[type] = extension;
    }

    public virtual void Update(float deltaTime)
    {
        Input.Sync();
        if (Screens.Count > 0)
            Screens[^1].Update(deltaTime);
        else if (Input.IsReceived(InputProvider.Signal.Back))
            OpenScreen(new IngameMenu(this));
        else
            World?.Update(deltaTime);
    }

    public void OpenScreen(Screen screen)
    {
        Screens.Add(screen);
    }

    public void CloseActiveScreen()
    {
        Screens.RemoveAt(Screens.Count - 1);
    }

    public void CloseAllScreens()
    {
        Screens.Clear();
    }

    protected void ShowTimedSplashScreen(SplashScreen.Type type, float seconds, Action? then = null)
    {
        var timer = 0f;
        OpenScreen(new SplashScreen(this, type, (screen, deltaTime) =>
        {
            timer += deltaTime;
            if (timer >= seconds)
            {
                CloseActiveScreen();
                then?.Invoke();
            }
        }));
    }

    public void ExitToMainMenu()
    {
        World = null;
        Screens.Clear();
        OpenScreen(new MainMenu(this));
    }

    public void GameOver(int score, bool win)
    {
        World?.Dispose();
        World = null;
        OpenScreen(new DialogScreen(
            this,
            win ? DialogScreen.Type.YouWin : DialogScreen.Type.GameOver,
            win ? $"Game Over!\nYour score: {score}" : $"You win!\nYour score: {score}",
            new[]
            {
                new DialogScreen.Button("Ok", screen =>
                {
                    CloseActiveScreen();
                    ExitToMainMenu();
                })
            }));
    }
}