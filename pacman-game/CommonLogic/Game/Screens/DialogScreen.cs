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

    public Button[] Buttons;

    public Type DialogType;
    public int SelectedButtonIndex;
    public string Text;

    public DialogScreen(Engine engine, Type dialogType, string text, Button[] buttons) : base(engine)
    {
        DialogType = dialogType;
        Text = text;
        Buttons = buttons;
        if (Buttons.Length == 0)
            throw new Exception("DialogScreen must have at least one button");
    }

    public override void Update(float deltaTime)
    {
        if (IsUp())
            SelectedButtonIndex = (SelectedButtonIndex + 1) % Buttons.Length;
        if (IsDown())
            SelectedButtonIndex = (SelectedButtonIndex + Buttons.Length - 1) % Buttons.Length;
        if (Engine.Input.IsReceived(InputProvider.Signal.Confirm))
            Buttons[SelectedButtonIndex].OnClick(this);
    }

    public struct Button
    {
        public string Text;
        public Action<DialogScreen> OnClick;

        public Button(string text, Action<DialogScreen> onClick)
        {
            Text = text;
            OnClick = onClick;
        }
    }
}