using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class SettingsMenu : MenuScreen
{
    public SettingsMenu(Engine engine) : base(engine)
    {
        Title = "Settings";
        Entries = new[]
        {
            new Entry("Audio",
                (MenuScreen screen, ref Entry entry) =>
                {
                    engine.AudioSystem.AudioEnabled = !engine.AudioSystem.AudioEnabled;
                },
                (MenuScreen screen, ref Entry entry) =>
                {
                    entry.SubText = engine.AudioSystem.AudioEnabled ? "On" : "Off";
                },
                description: "Configure audio settings", subText: engine.AudioSystem.AudioEnabled ? "On" : "Off"),
            new Entry("Keybindings",
                (MenuScreen screen, ref Entry entry) => { engine.OpenScreen(new KeybindingsMenu(engine)); },
                description: "Configure keys used in game"),
            new Entry("<- Back", (MenuScreen screen, ref Entry entry) => { engine.CloseActiveScreen(); },
                description: "Return to main menu")
        };
        BackAction = screen => { engine.CloseActiveScreen(); };
    }
}