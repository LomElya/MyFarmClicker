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

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopObjectView _immovablesObjectItemPrefab;
        private ShopObjectView _industryObjectItemPrefab;

        public ShopItemVisitor(ShopObjectView immovablesObjectItemPrefab, ShopObjectView industryObjectItemPrefab)
        {
            _immovablesObjectItemPrefab = immovablesObjectItemPrefab;
            _industryObjectItemPrefab = industryObjectItemPrefab;
        }

        public ShopObjectView Prefab { get; private set; }

        public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

        public void Visit(ImmovablesItemObject immovablesItemObject) => Prefab = _immovablesObjectItemPrefab;

        public void Visit(IndustryItemObject industryItemObject) => Prefab = _industryObjectItemPrefab;
    }
}
