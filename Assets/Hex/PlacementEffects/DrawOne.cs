using System.Collections.Generic;

public class DrawOne : TileBehaviour
{
    public override void OnTilePlaced(List<Tile> neighbours)
    {
        Draft.instance.DrawRandom(1);
    }
}