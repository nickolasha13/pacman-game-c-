using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public abstract class MenuScreen : Screen
{
    public delegate void EntryEvent(MenuScreen screen, ref Entry entry);
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
            this.Text = text;
            this.OnClick = onClick;
            this.OnUpdate = onUpdate;
            this.SubText = subText;
            this.Description = description;
        }
    }

    public string? Title;
    public Action<MenuScreen>? BackAction;
    public Entry[]? Entries;
    public int SelectedEntryIndex = 0;

    protected MenuScreen(Engine engine) : base(engine)
    {
    }

    public override void Update(float deltaTime)
    {
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Up) ||
            this.Engine.Input.IsReceived(InputProvider.Signal.Left))
            this.SelectedEntryIndex = (this.SelectedEntryIndex + this.Entries!.Length - 1) % this.Entries.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Down) ||
            this.Engine.Input.IsReceived(InputProvider.Signal.Right))
            this.SelectedEntryIndex = (this.SelectedEntryIndex + 1) % this.Entries!.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Confirm))
            this.Entries![this.SelectedEntryIndex].OnClick(this, ref this.Entries[this.SelectedEntryIndex]);
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Back))
            this.BackAction?.Invoke(this);
        for (var i = 0; i < this.Entries!.Length; i++)
            this.Entries[i].OnUpdate?.Invoke(this, ref this.Entries[i]);
    }
}
