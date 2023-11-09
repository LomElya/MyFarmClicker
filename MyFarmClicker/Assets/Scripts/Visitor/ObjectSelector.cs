public class ObjectSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public ObjectSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
        => _persistentData.PlayerData.BoughtImmovablesObject = immovablesItemObject.ObjectType;

    public void Visit(IndustryItemObject industryItemObject)
        => _persistentData.PlayerData.BoughtIndustrySubject = industryItemObject.ObjectType;
}
