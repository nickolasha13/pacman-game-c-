using CommonLogic.Game.Elements.Entity;

namespace CommonLogic.Core;

public class GameWorld: IDisposable
{
    public MapElement[,] Map;
    public List<EntityElement> Entities;
    public List<Routine> Routines = new();

    public Pacman Pacman;
    public Ghost[] Ghosts;

    public GameWorld(MapElement[,] map, Pacman pacman, Ghost ghostA, Ghost ghostB, Ghost ghostC, Ghost ghostD,
        List<EntityElement> entities)
    {
        Map = map;
        Entities = new List<EntityElement>();
        Pacman = pacman;
        Ghosts = new[] {ghostA, ghostB, ghostC, ghostD};
        
        foreach (var entity in entities)
        {
            AddEntity(entity);
        }

        AddEntity(pacman);
        AddEntity(ghostA);
        AddEntity(ghostB);
        AddEntity(ghostC);
        AddEntity(ghostD);
    }
    
    private bool _disposed = false;

    private bool _isUpdating = false;
    private List<EntityElement> _entitiesToRemove = new();

    public Vec2 Dimensions => new (Map.GetLength(1), Map.GetLength(0));
    
    public float GhostsFrightenedTime = 0;
    public bool IsGhostsFrightened => GhostsFrightenedTime > 0;
    
    public int Score = 0;
    public int Lives = 3;

    public void Update(float deltaTime)
    {
        if (IsGhostsFrightened) GhostsFrightenedTime -= Math.Min(deltaTime, GhostsFrightenedTime);
        
        _isUpdating = true;
        foreach (var routine in Routines)
        {
            routine.Update(deltaTime);
            if (_disposed) return;
        }
        foreach (var entity in Entities)
        {
            entity.Update(deltaTime);
            if (_disposed) return;
        }

        _isUpdating = false;
        foreach (var entity in _entitiesToRemove)
            PerformEntityRemoval(entity);
    }

    public void AddEntity(EntityElement entity)
    {
        Entities.Add(entity);
    }

    public void RemoveEntity(EntityElement entity)
    {
        if (_isUpdating) _entitiesToRemove.Add(entity);
        else PerformEntityRemoval(entity);
    }

    private void PerformEntityRemoval(EntityElement entity)
    {
        Entities.Remove(entity);
    }

    public bool CheckVictory()
    {
        foreach (var entity in Entities)
            if ((entity is Energizer or Coin) && !_entitiesToRemove.Contains(entity)) return false;
        
        return true;
    }
    
    public void Dispose()
    {
        _disposed = true;
    }
}
