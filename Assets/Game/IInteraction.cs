namespace Hex
{
public interface IInteraction
{
    bool OnTileClicked(Clickable clickable);
    void OnMouseWheel(int diff);
    void OnTileHovered(Tile tile);
}
}