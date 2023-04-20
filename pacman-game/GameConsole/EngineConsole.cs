using CommonLogic.Core;
using CommonLogic.Game.Screens;
using GameConsole.Extensions;
using GameConsole.Extensions.Render;
using GameConsole.Screens;

namespace GameConsole;

public class EngineConsole : Engine
{
    private Dictionary<Type, IScreenRenderer> _screenRenderers = new();
    
    public EngineConsole(ConsoleKeybindings keybindings) : base(new ConsoleInputProvider(keybindings))
    {
        RegisterExtension(new RenderWall());
        RegisterExtension(new RenderFloor());
        RegisterExtension(new RenderPacman());
        RegisterExtension(new RenderCoin());
        RegisterExtension(new RenderEnergizer());
        RegisterExtension(new RenderGhost());
        
        this._screenRenderers.Add(typeof(SplashScreen), new SplashRenderer());
        this._screenRenderers.Add(typeof(MenuScreen), new MenuRenderer());
        this._screenRenderers.Add(typeof(DialogScreen), new DialogRenderer());
    }

    private void Render(float deltaTime)
    {
        var windowWidth = Console.WindowWidth;
        var windowHeight = Console.WindowHeight;
        var buffer = new Symbol?[windowHeight, windowWidth];
        if (World != null && Screens.Count == 0)
        {
            var shiftX = windowWidth / 2 - World.Map.GetLength(1) / 2;
            var shiftY = windowHeight / 2 - World.Map.GetLength(0) / 2;
            for (var y = 0; y < World.Map.GetLength(0); y++)
            {
                for (var x = 0; x < World.Map.GetLength(1); x++)
                {
                    var element = World.Map[y, x];
                    if (!Extensions.ContainsKey(element.GetType()))
                        continue;
                    foreach (var ext in Extensions[element.GetType()])
                    {
                        if (ext.GetType().IsInstanceOfType(typeof(RenderExtension<>)))
                            continue;
                        
                        var result = ((IRenderExtension) ext).RenderElement(element, this, deltaTime, x, y);
                        foreach (var symbol in result)
                        {
                            if (symbol.X + shiftX < 0 || symbol.X + shiftX >= windowWidth || symbol.Y + shiftY < 0 || symbol.Y + shiftY >= windowHeight)
                                continue;
                            buffer[symbol.Y + shiftY, symbol.X + shiftX] = symbol;
                        }
                    }
                }
            }
            foreach (var element in World.Entities)
            {
                if (!Extensions.ContainsKey(element.GetType()))
                    continue;
                foreach (var ext in Extensions[element.GetType()])
                {
                    if (ext.GetType().IsInstanceOfType(typeof(RenderExtension<>)))
                        continue;
                    
                    var result = ((IRenderExtension) ext).RenderElement(element, this, deltaTime, element.Position.X, element.Position.Y);
                    foreach (var symbol in result)
                    {
                        if (symbol.X + shiftX < 0 || symbol.X + shiftX >= windowWidth || symbol.Y + shiftY < 0 || symbol.Y + shiftY >= windowHeight)
                            continue;
                        buffer[symbol.Y + shiftY, symbol.X + shiftX] = symbol;
                    }
                }
            }
            var textLines = new List<string>();
            textLines.Add($"FPS: {(int)(1.0 / deltaTime)}");
            textLines.Add($"Score: {World.Score}");
            textLines.Add($"Lives: {World.Lives}");
            for (var i = 0; i < textLines.Count; i++)
            {
                var line = textLines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    buffer[i, j] = new Symbol(line[j], j, i, ConsoleColor.White, ConsoleColor.Black);
                }
            }
        }
        if (Screens.Count > 0)
        {
            var screen = Screens[^1];
            if (_screenRenderers.ContainsKey(screen.GetType()))
            {
                _screenRenderers[screen.GetType()].Render(screen, new Vec2(windowWidth, windowHeight), buffer);
            }
        }

        for (var y = 0; y < windowHeight; y++)
        {
            for (var x = 0; x < windowWidth; x++)
            {
                var symbol = buffer[y, x];
                if (symbol == null)
                {
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
                else
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = symbol.Value.Foreground ?? Console.ForegroundColor;
                    Console.BackgroundColor = symbol.Value.Background ?? Console.BackgroundColor;
                    Console.Write(symbol.Value.Character);
                }
            }
        }
    }
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        Render(deltaTime);
    }
}
