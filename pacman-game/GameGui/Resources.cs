using SFML.Graphics;

namespace GameGui;

public class Resources
{
    private static readonly Dictionary<string, Font> _fontCache = new();
    private static readonly Dictionary<string, Texture> _textureCache = new();

    static Resources()
    {
        foreach (var f in Directory.GetFiles("./Assets/fonts", "*.ttf"))
            _fontCache.Add(Path.GetFileNameWithoutExtension(f), new Font(f));

        foreach (var s in Directory.GetFiles("./Assets/sprites", "*.png"))
            _textureCache.Add(Path.GetFileNameWithoutExtension(s), new Texture(s));
    }

    public static Font Font(string name)
    {
        return _fontCache[name];
    }

    public static Texture Texture(string name)
    {
        return _textureCache[name];
    }
}