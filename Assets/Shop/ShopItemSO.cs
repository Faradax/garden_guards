using UnityEngine;

namespace DefaultNamespace.Shop
{
[CreateAssetMenu(fileName = "ShopItem", menuName = "Garden Guards/Shop Item", order = 0)]
public class ShopItemSO : ScriptableObject
{
    [Min(0)]
    public int price;
    public GameObject previewAsset;
}
}