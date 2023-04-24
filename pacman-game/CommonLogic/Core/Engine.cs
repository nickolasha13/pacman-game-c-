using CommonLogic.Game.Screens;

namespace CommonLogic.Core;

public abstract class Engine
{
    public InputProvider Input;
    public Dictionary<Type, List<IElementExtension>> Extensions = new();
    public GameWorld? World;
    public List<Screen> Screens = new();

    protected Engine(InputProvider input)
    {
        this.Input = input;
        ShowTimedSplashScreen(SplashScreen.Type.Intro, 1, ExitToMainMenu);
    }
    
    public void RegisterExtension(IElementExtension extension)
    {
        foreach (var type in extension.ElementTypes())
        {
            if (!Extensions.ContainsKey(type))
                Extensions.Add(type, new List<IElementExtension>());
            Extensions[type].Add(extension);
        }
    }
    
    public virtual void Update(float deltaTime)
    {
        Input.Sync();
        if (Screens.Count > 0) this.Screens[^1].Update(deltaTime);
        else this.World?.Update(deltaTime);
    }
    
    public void OpenScreen(Screen screen)
    {
        this.Screens.Add(screen);
    }
    
    public void CloseActiveScreen()
    {
        this.Screens.RemoveAt(this.Screens.Count - 1);
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
        this.World = null;
        this.Screens.Clear();
        OpenScreen(new MenuScreen(this, "Main Menu", new []
        {
            new MenuScreen.Entry("Start Game", (screen) =>
            {
                var level = File.ReadAllText("Levels/lvl1.txt");
                var world = MapLoader.LoadMap(this, level);
                this.World = world;
                CloseActiveScreen();
            }),
            new MenuScreen.Entry("Exit", (screen) =>
            {
                Console.ResetColor();
                Environment.Exit(0); // TODO: Implement proper exit
            })
        }));
    }
    
    public void GameOver(int score, bool win)
    {
        this.World?.Dispose();
        this.World = null;
        this.OpenScreen(new DialogScreen(
            this,
            win ? DialogScreen.Type.YouWin : DialogScreen.Type.GameOver, 
            win ? $"Game Over!\nYour score: {score}" : $"You win!\nYour score: {score}",
            new[]
        {
            new DialogScreen.Button("Ok", (screen) =>
            {
                CloseActiveScreen();
                ExitToMainMenu();
            })
        }));
    }
}
