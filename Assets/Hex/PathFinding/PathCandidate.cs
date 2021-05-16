using System.Collections.Generic;
using System.Linq;

public class PathCandidate
{
    public IReadOnlyList<Tile> path;

    public int Length => path.Count;

    public Tile Tip => path.Last();
    
    public PathCandidate(Tile start)
    {
        this.path = new List<Tile> {start};
    }
    
    public PathCandidate(IReadOnlyList<Tile> path)
    {
        this.path = new List<Tile>(path);
    }

    public PathCandidate ExtendedBy(Tile tile)
    {
        List<Tile> extendedPath = new List<Tile>(path);
        if (!path.Contains(tile))
        {
            extendedPath.Add(tile);
        }
        return new PathCandidate(extendedPath);
    }

    public override string ToString()
    {
        string aggregate = path.Select(tile => tile.gameObject.name).Aggregate((s, s1) => s + s1);
        return aggregate;
    }
}