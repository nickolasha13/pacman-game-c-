using CommonLogic.Core;
using CommonLogic.Game.Screens;
using GameGui.Extensions;
using GameGui.Extensions.Render;
using GameGui.Screens;
using SFML.Graphics;
using SFML.System;
using IRenderExtension = GameGui.Extensions.IRenderExtension;

namespace GameGui;

public class EngineGui : Engine
{
    private readonly Dictionary<Type, IScreenRenderer> _screenRenderers = new();
    private readonly RenderWindow _window;
    private int _fps;

    private int _fpsCounter;
    private float _fpsTimer;

    public EngineGui(RenderWindow window, GuiInputProvider input) : base(input)
    {
        _window = window;
        RegisterExtension(new RenderWall());
        RegisterExtension(new RenderFloor());
        RegisterExtension(new RenderFruit());
        RegisterExtension(new RenderPacman());
        RegisterExtension(new RenderCoin());
        RegisterExtension(new RenderEnergizer());
        RegisterExtension(new RenderGhost());

        _screenRenderers.Add(typeof(SplashScreen), new SplashRenderer());
        _screenRenderers.Add(typeof(MenuScreen), new MenuRenderer());
        _screenRenderers.Add(typeof(DialogScreen), new DialogRenderer());
    }

    private void Render(float deltaTime)
    {
        _fpsCounter++;
        _fpsTimer += deltaTime;
        if (_fpsTimer >= 1)
        {
            _fps = _fpsCounter;
            _fpsCounter = 0;
            _fpsTimer = 0;
        }

        var windowWidth = (int)_window.Size.X;
        var windowHeight = (int)_window.Size.Y;

        var gridWidth = windowWidth / 16;
        var gridHeight = windowHeight / 16;
        var gridOffsetX = (windowWidth - gridWidth * 16) / 2;
        var gridOffsetY = (windowHeight - gridHeight * 16) / 2;
        if (World != null && Screens.Count == 0)
        {
            var shiftX = windowWidth / 2 - World.Map.GetLength(1) * 16 / 2 + gridOffsetX;
            var shiftY = windowHeight / 2 - World.Map.GetLength(0) * 16 / 2 + gridOffsetY;
            for (var y = 0; y < World.Map.GetLength(0); y++)
            for (var x = 0; x < World.Map.GetLength(1); x++)
            {
                var element = World.Map[y, x];
                if (!Extensions.ContainsKey(element.GetType()))
                    continue;
                var ext = Extensions[element.GetType()];
                if (ext.GetType().IsInstanceOfType(typeof(RenderExtension<>)))
                    continue;

                var result = ((IRenderExtension)ext).RenderElement(element, this, deltaTime, x, y);
                if (result == null)
                    continue;
                var sprite = new Sprite(result);
                sprite.Scale = new Vector2f(0.5f, 0.5f);
                sprite.Position = new Vector2f(x * 16 + shiftX, y * 16 + shiftY);
                _window.Draw(sprite);
            }

            foreach (var element in World.Entities)
            {
                if (!Extensions.ContainsKey(element.GetType()))
                    continue;
                var ext = Extensions[element.GetType()];

                if (ext.GetType().IsInstanceOfType(typeof(RenderExtension<>)))
                    continue;

                var result = ((IRenderExtension)ext).RenderElement(element, this, deltaTime, element.Position.X,
                    element.Position.Y);
                if (result == null)
                    continue;
                var sprite = new Sprite(result);
                sprite.Scale = new Vector2f(0.5f, 0.5f);
                sprite.Position = new Vector2f(element.Position.X * 16 + shiftX, element.Position.Y * 16 + shiftY);
                _window.Draw(sprite);
            }

            var textLines = new List<string>();
            textLines.Add($"FPS: {_fps}");
            textLines.Add($"Score: {World.Score}");
            textLines.Add($"Lives: {World.Lives}");
            for (var i = 0; i < textLines.Count; i++)
            {
                var line = textLines[i];
                var text = new Text(line, Resources.Font("PressStart2P"), 16);
                text.Position = new Vector2f(0, i * 16);
                _window.Draw(text);
            }
        }

        if (Screens.Count > 0)
        {
            var screen = Screens[^1];
            IScreenRenderer? screenRenderer;
            if (!_screenRenderers.TryGetValue(screen.GetType(), out screenRenderer))
            {
                var baseType = screen.GetType().BaseType;
                while (baseType != null && !_screenRenderers.TryGetValue(baseType, out screenRenderer))
                    baseType = baseType.BaseType;
            }

            screenRenderer?.Render(screen, new Vec2(windowWidth, windowHeight), _window);
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        Render(deltaTime);
    }
}