using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public abstract class MenuScreen : Screen
{
    public delegate void EntryEvent(MenuScreen screen, ref Entry entry);

    public Action<MenuScreen>? BackAction;
    public Entry[]? Entries;
    public int SelectedEntryIndex;

    public string? Title;

    protected MenuScreen(Engine engine) : base(engine)
    {
    }

    public override void Update(float deltaTime)
    {
        if (IsUp())
            SelectedEntryIndex = (SelectedEntryIndex + Entries!.Length - 1) % Entries.Length;
        if (IsDown())
            SelectedEntryIndex = (SelectedEntryIndex + 1) % Entries!.Length;
        if (Engine.Input.IsReceived(InputProvider.Signal.Confirm))
            Entries![SelectedEntryIndex].OnClick(this, ref Entries[SelectedEntryIndex]);
        if (Engine.Input.IsReceived(InputProvider.Signal.Back))
            BackAction?.Invoke(this);
        for (var i = 0; i < Entries!.Length; i++)
            Entries[i].OnUpdate?.Invoke(this, ref Entries[i]);
    }

    public struct Entry
    {
        public string Text;
        public EntryEvent OnClick;
        public EntryEvent? OnUpdate;
        public string? SubText;
        public string? Description;

        public Entry(string text, EntryEvent onClick, EntryEvent? onUpdate = null,
            string? subText = null, string? description = null)
        {
            Text = text;
            OnClick = onClick;
            OnUpdate = onUpdate;
            SubText = subText;
            Description = description;
        }
    }
}