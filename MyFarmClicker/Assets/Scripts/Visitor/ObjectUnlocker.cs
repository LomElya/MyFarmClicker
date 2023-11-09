public class ObjectUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public ObjectUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
        => _persistentData.PlayerData.OpenImmovablesObject(immovablesItemObject.ObjectType);

    public void Visit(IndustryItemObject industryItemObject)
        => _persistentData.PlayerData.OpenIndustrySubject(industryItemObject.ObjectType);
}
