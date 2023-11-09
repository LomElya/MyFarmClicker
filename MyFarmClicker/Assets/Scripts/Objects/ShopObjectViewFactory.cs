using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopObjectViewFactory", menuName = "Shop/ShopObjectViewFactory")]
public class ShopObjectViewFactory : ScriptableObject
{
    [SerializeField] private ShopObjectView _immovablesObjectItemPrefab;
    [SerializeField] private ShopObjectView _industryObjectItemPrefab;

    public ShopObjectView Get(ShopObject shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_immovablesObjectItemPrefab, _industryObjectItemPrefab);
        visitor.Visit(shopItem);

        ShopObjectView instance = Instantiate(visitor.Prefab, parent);
        instance.Init(shopItem);

        return instance;
    }
}
