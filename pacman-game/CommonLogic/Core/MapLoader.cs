using CommonLogic.Game.Elements.Entity;
using CommonLogic.Game.Elements.Entity.Ghosts;
using CommonLogic.Game.Elements.Map;
using CommonLogic.Game.Routines;

namespace CommonLogic.Core;

public class MapLoader
{
    private static readonly Dictionary<char, Func<Engine, Vec2, List<Element>>> _elementConstructors = new();

    static MapLoader()
    {
        _elementConstructors.Add(' ', (e, _) => new List<Element> { new Floor(e) });
        _elementConstructors.Add('#', (e, _) => new List<Element> { new Wall(e) });
        _elementConstructors.Add('·', (e, p) => new List<Element> { new Coin(e, p), new Floor(e) });
        _elementConstructors.Add('+', (e, p) => new List<Element> { new Energizer(e, p), new Floor(e) });
        _elementConstructors.Add('@', (e, p) => new List<Element> { new Pacman(e, p), new Floor(e) });
        _elementConstructors.Add('A', (e, p) => new List<Element> { new Blinky(e, p), new Floor(e) });
        _elementConstructors.Add('B', (e, p) => new List<Element> { new Pinky(e, p), new Floor(e) });
        _elementConstructors.Add('C', (e, p) => new List<Element> { new Inky(e, p), new Floor(e) });
        _elementConstructors.Add('D', (e, p) => new List<Element> { new Clyde(e, p), new Floor(e) });
    }

    public static GameWorld LoadMap(Engine engine, string data)
    {
        var lines = data.ReplaceLineEndings().Split('\n');
        var metadata = parseMetadata(lines[0]);
        var width = int.Parse(metadata["width"]);
        var height = int.Parse(metadata["height"]);

        var initialShift = 1;
        var map = new MapElement[height, width];
        Pacman? pacman = null;
        var ghosts = new Ghost[4];
        var entities = new List<EntityElement>();

        for (var y = 0; y < height; y++)
        {
            var line = lines[y + initialShift];
            for (var x = 0; x < width; x++)
            {
                var c = line[x];
                if (!_elementConstructors.ContainsKey(c))
                    continue;
                foreach (var element in _elementConstructors[c](engine, new Vec2(x, y)))
                    if (element is Pacman p)
                        pacman = p;
                    else if (element is Blinky g1)
                        ghosts[0] = g1;
                    else if (element is Pinky g2)
                        ghosts[1] = g2;
                    else if (element is Inky g3)
                        ghosts[2] = g3;
                    else if (element is Clyde g4)
                        ghosts[3] = g4;
                    else if (element is EntityElement entity)
                        entities.Add(entity);
                    else if (element is MapElement tile)
                        map[y, x] = tile;
                    else
                        throw new Exception("Invalid element type: " + element.GetType().Name);
            }
        }

        var gw = new GameWorld(map, pacman!, ghosts[0], ghosts[1], ghosts[2], ghosts[3], entities);
        gw.Routines.Add(new FruitSpawnRoutine(engine));
        return gw;
    }

    private static Dictionary<string, string> parseMetadata(string data)
    {
        var dict = new Dictionary<string, string>();
        var pairs = data.Split('&');
        foreach (var pair in pairs)
        {
            var split = pair.Split('=');
            dict.Add(split[0], split[1]);
        }

        return dict;
    }
}