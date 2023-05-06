using CommonLogic.Game.Elements.Entity;
using CommonLogic.Game.Elements.Entity.Ghosts;
using CommonLogic.Game.Elements.Map;

namespace CommonLogic.Core;

public class MapLoader
{
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
                switch (c)
                {
                    case ' ':
                        map[y, x] = new Floor(engine);
                        break;
                    case '#':
                        map[y, x] = new Wall(engine);
                        break;
                    case '·':
                        entities.Add(new Coin(engine, new Vec2(x, y)));
                        map[y, x] = new Floor(engine);
                        break;
                    case '+':
                        entities.Add(new Energizer(engine, new Vec2(x, y)));
                        map[y, x] = new Floor(engine);
                        break;
                    case '@':
                        pacman = new Pacman(engine, new Vec2(x, y));
                        map[y, x] = new Floor(engine);
                        break;
                    case 'A':
                        ghosts[0] = new Blinky(engine, new Vec2(x, y));
                        map[y, x] = new Floor(engine);
                        break;
                    case 'B':
                        ghosts[1] = new Pinky(engine, new Vec2(x, y));
                        map[y, x] = new Floor(engine);
                        break;
                    case 'C':
                        ghosts[2] = new Inky(engine, new Vec2(x, y));
                        map[y, x] = new Floor(engine);
                        break;
                    case 'D':
                        ghosts[3] = new Clyde(engine, new Vec2(x, y));
                        map[y, x] = new Floor(engine);
                        break;
                    default:
                        throw new Exception("Invalid character in map: '" + c + "'");
                }
            }
        }

        return new GameWorld(map, pacman!, ghosts[0], ghosts[1], ghosts[2], ghosts[3], entities);
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
