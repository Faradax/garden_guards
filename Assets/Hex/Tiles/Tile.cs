using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject _thingOnTop;

    public bool IsFree()
    {
        return _thingOnTop == null;
    }

    public void SpawnTower(TowerSO towerSo)
    {
        GameObject asset = Instantiate(towerSo.asset);
        asset.transform.position = transform.position;
    }
}