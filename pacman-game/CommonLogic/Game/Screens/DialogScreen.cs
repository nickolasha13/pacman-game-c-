using CommonLogic.Core;

namespace CommonLogic.Game.Screens;

public class DialogScreen : Screen
{
    public enum Type
    {
        Regular,
        GameOver,
        YouWin
    }
    
    public struct Button
    {
        public string Text;
        public Action<DialogScreen> OnClick;
        
        public Button(string text, Action<DialogScreen> onClick)
        {
            this.Text = text;
            this.OnClick = onClick;
        }
    }

    public Type DialogType;
    public string Text;
    public Button[] Buttons;
    public int SelectedButtonIndex = 0;

    public DialogScreen(Engine engine, Type dialogType, string text, Button[] buttons) : base(engine)
    {
        this.DialogType = dialogType;
        this.Text = text;
        this.Buttons = buttons;
        if (this.Buttons.Length == 0)
        {
            throw new Exception("DialogScreen must have at least one button");
        }
    }

    public override void Update(float deltaTime)
    {
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Up) || this.Engine.Input.IsReceived(InputProvider.Signal.Left))
            this.SelectedButtonIndex = (this.SelectedButtonIndex + 1) % this.Buttons.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Down) || this.Engine.Input.IsReceived(InputProvider.Signal.Right))
            this.SelectedButtonIndex = (this.SelectedButtonIndex + this.Buttons.Length - 1) % this.Buttons.Length;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Confirm))
            this.Buttons[this.SelectedButtonIndex].OnClick(this);
    }
}
