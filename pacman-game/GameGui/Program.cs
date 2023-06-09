using CommonLogic.Core;
using GameGui;
using SFML.Graphics;
using SFML.Window;

var initialSize = new Vec2(800, 600);

var vm = new VideoMode((uint)initialSize.X, (uint)initialSize.Y);
var window = new RenderWindow(vm, "Pacman", Styles.Default);

var icon = Resources.Texture("icon");
window.SetIcon(icon.Size.X, icon.Size.Y, icon.CopyToImage().Pixels);

window.SetVerticalSyncEnabled(true);
window.Closed += (sender, args) => window.Close();
window.Resized += (sender, args) =>
{
    var size = new Vec2((int)args.Width, (int)args.Height);
    window.SetView(new View(new FloatRect(0, 0, size.X, size.Y)));
};

var input = new GuiInputProvider(new GuiKeybindings());

window.KeyPressed += (sender, args) =>
{
    input.SubmitKey(args.Code);
};
window.JoystickMoved += (sender, args) =>
{
    if (args.Axis == Joystick.Axis.PovX)
    {
        if (args.Position <= -1)
        {
            input.SubmitKey(Keyboard.Key.Left);
        }
        else if (args.Position >= 1)
        {
            input.SubmitKey(Keyboard.Key.Right);
        }
    }
    else if (args.Axis == Joystick.Axis.PovY)
    {
        if (args.Position <= -1)
        {
            input.SubmitKey(Keyboard.Key.Up);
        }
        else if (args.Position >= 1)
        {
            input.SubmitKey(Keyboard.Key.Down);
        }
    }
};
window.JoystickButtonPressed += (sender, args) =>
{
    if (args.Button == 0)
    {
        input.SubmitKey(Keyboard.Key.Enter);
    }
    else if (args.Button == 1)
    {
        input.SubmitKey(Keyboard.Key.Escape);
    }
};

var engine = new EngineGui(window, input);

var lastTime = DateTime.Now;
while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    
    var now = DateTime.Now;
    var deltaTime = (float)(now - lastTime).TotalSeconds;
    engine.Update(deltaTime);
    lastTime = now;
    
    window.Display();
}
