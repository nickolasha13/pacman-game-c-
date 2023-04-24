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
    
    private int _fpsCounter = 0;
    private int _fps = 0;
    private float _fpsTimer = 0;
    
    private void Render(float deltaTime)
    {
        this._fpsCounter++;
        this._fpsTimer += deltaTime;
        if (_fpsTimer >= 1)
        {
            this._fps = _fpsCounter;
            this._fpsCounter = 0;
            this._fpsTimer = 0;
        }
        
        var windowWidth = Console.WindowWidth;
        var windowHeight = Console.WindowHeight;
        var buffer = new Symbol?[windowHeight, windowWidth];
        if (this.World != null && this.Screens.Count == 0)
        {
            var shiftX = windowWidth / 2 - this.World.Map.GetLength(1) / 2;
            var shiftY = windowHeight / 2 - this.World.Map.GetLength(0) / 2;
            for (var y = 0; y < this.World.Map.GetLength(0); y++)
            {
                for (var x = 0; x < this.World.Map.GetLength(1); x++)
                {
                    var element =this. World.Map[y, x];
                    if (!this.Extensions.ContainsKey(element.GetType()))
                        continue;
                    foreach (var ext in this.Extensions[element.GetType()])
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
                if (!this.Extensions.ContainsKey(element.GetType()))
                    continue;
                foreach (var ext in this.Extensions[element.GetType()])
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
            textLines.Add($"FPS: {this._fps}");
            textLines.Add($"Score: {World.Score}");
            textLines.Add($"Lives: {World.Lives}");
            for (var i = 0; i < textLines.Count; i++)
            {
                var line = textLines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    buffer[i, j] = new Symbol(line[j], j, i, 255, 0);
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

        var strBuff = new string[windowHeight];
        for (var i = 0; i < windowHeight; i++) strBuff[i] = "\x1b[48;5;0m\x1b[38;5;0m";
        byte? currentForeground = 0;
        byte? currentBackground = 0;
        for (var y = 0; y < windowHeight; y++)
        {
            for (var x = 0; x < windowWidth; x++)
            {
                var symbol = buffer[y, x];
                if (symbol == null)
                {
                    if (currentBackground != 0)
                    {
                        strBuff[y] += "\x1b[48;5;0m";
                        currentBackground = 0;
                    }
                    if (currentForeground != 0)
                    {
                        strBuff[y] += "\x1b[38;5;0m";
                        currentForeground = 0;
                    }
                    strBuff[y] += ' ';
                }
                else
                {
                    if (symbol.Value.Background != currentBackground && symbol.Value.Background != null)
                    {
                        if (symbol.Value.Background != null)
                        {
                            strBuff[y] += "\x1b[48;5;" + symbol.Value.Background + "m";
                            currentBackground = symbol.Value.Background;
                        }
                        else
                        {
                            strBuff[y] += "\x1b[48;5;0m";
                            currentBackground = null;
                        }
                    }
                    if (symbol.Value.Foreground != currentForeground)
                    {
                        if (symbol.Value.Foreground != null)
                        {
                            strBuff[y] += "\x1b[38;5;" + symbol.Value.Foreground + "m";
                            currentForeground = symbol.Value.Foreground;
                        }
                        else
                        {
                            strBuff[y] += "\x1b[38;5;0m";
                            currentForeground = null;
                        }
                    }
                    strBuff[y] += symbol.Value.Character;
                }
            }
        }
        for (var i = 0; i < windowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(strBuff[i]);
            Console.Out.Flush();
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        Render(deltaTime);
    }
}
