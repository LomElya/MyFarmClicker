public class ShopItemVisitor : IShopItemVisitor
{
    private ShopObjectView _immovablesObjectItemPrefab;
    private ShopObjectView _industryObjectItemPrefab;

    public ShopItemVisitor(ShopObjectView immovablesObjectItemPrefab, ShopObjectView industryObjectItemPrefab)
    {
        _immovablesObjectItemPrefab = immovablesObjectItemPrefab;
        _industryObjectItemPrefab = industryObjectItemPrefab;
    }

    public ShopObjectView Prefab { get; private set; }
    public string TypeObject { get; private set; }

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
    {
        Prefab = _immovablesObjectItemPrefab;
        TypeObject = immovablesItemObject.ObjectType.ToString();
    }

    public void Visit(IndustryItemObject industryItemObject)
    {
        Prefab = _industryObjectItemPrefab;
        TypeObject = industryItemObject.ObjectType.ToString();
    }
}