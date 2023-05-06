using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class IngameMenu : MenuScreen
{
    public IngameMenu(Engine engine) : base(engine)
    {
        this.Title = "Paused";
        this.Entries = new[]
        {
            new Entry("Continue", (MenuScreen screen, ref Entry entry) =>
            {
                engine.CloseActiveScreen();
            }, description: "Return to the game"),
            new Entry("Settings",
                (MenuScreen screen, ref Entry entry) => { engine.OpenScreen(new SettingsMenu(engine)); },
                description: "Change game settings"),
            new Entry("Exit To Main Menu", (MenuScreen screen, ref Entry entry) =>
            {
                engine.ExitToMainMenu();
            }, description: "Exit to the main menu"),
        };
        this.BackAction = (screen) => { engine.CloseActiveScreen(); };
    }
}
