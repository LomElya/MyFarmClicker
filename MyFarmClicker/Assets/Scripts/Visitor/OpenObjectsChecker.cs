using System.Linq;

public class OpenObjectsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    public OpenObjectsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
        => IsOpened = _persistentData.PlayerData.OpenImmovablesObjects.Contains(immovablesItemObject.ObjectType);

    public void Visit(IndustryItemObject industryItemObject)
       => IsOpened = _persistentData.PlayerData.OpenIndustrySubjects.Contains(industryItemObject.ObjectType);
}