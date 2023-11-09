public class BoughtObjectChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsBought { get; private set; }

    public BoughtObjectChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
        => IsBought = _persistentData.PlayerData.BoughtImmovablesObject == immovablesItemObject.ObjectType;

    public void Visit(IndustryItemObject industryItemObject)
       => IsBought = _persistentData.PlayerData.BoughtIndustrySubject == industryItemObject.ObjectType;
}
