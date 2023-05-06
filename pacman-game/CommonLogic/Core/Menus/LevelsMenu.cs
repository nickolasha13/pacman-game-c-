using CommonLogic.Game.Screens;

namespace CommonLogic.Core.Menus;

public class LevelsMenu : MenuScreen
{
    public LevelsMenu(Engine engine) : base(engine)
    {
        this.Title = "Select Level";
        var files = Directory.GetFiles("Levels");
        Array.Sort(files);
        this.Entries = new Entry[files.Length + 1];
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            this.Entries[i] = new Entry(
                Path.GetFileNameWithoutExtension(file),
                (MenuScreen screen, ref Entry entry) =>
                {
                    var level = File.ReadAllText(file);
                    engine.World = MapLoader.LoadMap(engine, level);
                    engine.CloseAllScreens();
                },
                description: "Select level to play"
            );
        }
        this.Entries[^1] = new Entry("<- Back", (MenuScreen screen, ref Entry entry) => { engine.CloseActiveScreen(); },
            description: "Return to main menu");

        this.BackAction = (screen) => { engine.CloseActiveScreen(); };
    }
}
