using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public class MenuScreen : Screen
{
    public struct Entry
    {
        public string Text;
        public Action<MenuScreen> OnClick;
        public string? SubText;
        public string? Description;
        
        public Entry(string text, Action<MenuScreen> onClick, string? subText = null, string? description = null)
        {
            this.Text = text;
            this.OnClick = onClick;
            this.SubText = subText;
            this.Description = description;
        }
    }

    public string Title;
    public Action<MenuScreen>? BackAction;
    public Entry[] Entries;
    public int SelectedEntryIndex = 0;

    public MenuScreen(Engine engine, string title, Entry[] entries, Action<MenuScreen>? backAction = null) : base(engine)
    {
        this.Title = title;
        this.Entries = entries;
        if (this.Entries.Length == 0)
        {
            throw new Exception("MenuScreen must have at least one entry");
        }
    }

    public override void Update(float deltaTime)
    {
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Up) || this.Engine.Input.IsReceived(InputProvider.Signal.Left))
            this.SelectedEntryIndex = (this.SelectedEntryIndex + 1) % this.Entries.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Down) || this.Engine.Input.IsReceived(InputProvider.Signal.Right))
            this.SelectedEntryIndex = (this.SelectedEntryIndex + this.Entries.Length - 1) % this.Entries.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Confirm))
            this.Entries[this.SelectedEntryIndex].OnClick(this);
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Back))
            this.BackAction?.Invoke(this);
    }
}
