using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class MainMenu : MenuScreen
{
    public MainMenu(Engine engine) : base(engine)
    {
        Title = "Main Menu";
        Entries = new[]
        {
            new Entry("Start Game",
                (MenuScreen screen, ref Entry entry) => { engine.OpenScreen(new LevelsMenu(engine)); },
                description: "Select level to play"),
            new Entry("Settings",
                (MenuScreen screen, ref Entry entry) => { engine.OpenScreen(new SettingsMenu(engine)); },
                description: "Change game settings"),
            new Entry("Exit", (MenuScreen screen, ref Entry entry) =>
            {
                Console.ResetColor();
                Environment.Exit(0); // TODO: Implement proper exit
            }, description: "Exit the game")
        };
    }
}