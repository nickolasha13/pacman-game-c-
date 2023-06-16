using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class KeybindingsMenu : MenuScreen
{
    public KeybindingsMenu(Engine engine) : base(engine)
    {
        Title = "Keybindings";
        var entries = new List<Entry>();
        foreach (var signal in Enum.GetValues<InputProvider.Signal>())
        {
            var signalName = signal.ToString();
            var signalDescription = signal switch
            {
                InputProvider.Signal.Up => "Move up",
                InputProvider.Signal.Down => "Move down",
                InputProvider.Signal.Left => "Move left",
                InputProvider.Signal.Right => "Move right",
                InputProvider.Signal.Confirm => "Confirm",
                InputProvider.Signal.Back => "Back",
                _ => throw new ArgumentOutOfRangeException()
            };
            var rebinding = false;
            entries.Add(new Entry(signalName, (MenuScreen screen, ref Entry entry) =>
            {
                engine.Input.Rebind(signal);
                rebinding = true;
                entry.SubText = "Press a key...";
            }, (MenuScreen screen, ref Entry entry) =>
            {
                if (rebinding && engine.Input.GetAwaitingRebind() == null)
                {
                    rebinding = false;
                    entry.SubText = "[" + engine.Input.GetSignalBindingAsString(signal) + "]";
                }
            }, description: signalDescription, subText: "[" + engine.Input.GetSignalBindingAsString(signal) + "]"));
        }

        entries.Add(new Entry("<- Back", (MenuScreen screen, ref Entry entry) => { engine.CloseActiveScreen(); },
            description: "Return to settings menu"));

        Entries = entries.ToArray();
        BackAction = screen => { engine.CloseActiveScreen(); };
    }
}