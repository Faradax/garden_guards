using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draft: MonoBehaviour
{

    public TowerSO[] pool;
    public List<TowerSO> current;
    private int _index;

    public event Action draftRefresh;
    public event Action<TowerSO> selectionChanged;
    
    public void OnClickableSelected(Clickable target)
    {
        TowerSO towerSo = current[_index];
        GameObject asset = Instantiate(towerSo.asset);
        current.RemoveAt(_index);
        asset.transform.position = target.transform.position;
        draftRefresh?.Invoke();
    }

    public IEnumerable<TowerSO> GetItems()
    {
        return current;
    }

    public void ChangeSelection(int index)
    {
        _index = index;
    }
}