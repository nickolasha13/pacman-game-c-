using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class IngameMenu : MenuScreen
{
    public IngameMenu(Engine engine) : base(engine)
    {
        Title = "Paused";
        Entries = new[]
        {
            new Entry("Continue", (MenuScreen screen, ref Entry entry) => { engine.CloseActiveScreen(); },
                description: "Return to the game"),
            new Entry("Settings",
                (MenuScreen screen, ref Entry entry) => { engine.OpenScreen(new SettingsMenu(engine)); },
                description: "Change game settings"),
            new Entry("Exit To Main Menu", (MenuScreen screen, ref Entry entry) => { engine.ExitToMainMenu(); },
                description: "Exit to the main menu")
        };
        BackAction = screen => { engine.CloseActiveScreen(); };
    }
}