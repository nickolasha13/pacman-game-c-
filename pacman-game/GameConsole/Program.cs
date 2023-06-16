using GameConsole;

var engine = new EngineConsole(new ConsoleKeybindings());

Console.CursorVisible = false;

var lastTime = DateTime.Now;
while (true)
{
    var now = DateTime.Now;
    var deltaTime = (float)(now - lastTime).TotalSeconds;
    engine.Update(deltaTime);
    lastTime = now;
}