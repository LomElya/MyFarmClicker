public class CountItemVisitor : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public int Count { get; private set; }

    public CountItemVisitor(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject) =>
        Count = _persistentData.PlayerData.CountImmovablesObject(immovablesItemObject.ObjectType);

    public void Visit(IndustryItemObject industryItemObject) =>
        Count = _persistentData.PlayerData.CountIndustrySubject(industryItemObject.ObjectType);
}