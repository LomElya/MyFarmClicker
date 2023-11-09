using System.Linq;

public class CountObjectsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public int Count { get; private set; }
    public float Profit { get; private set; }
    public float Coefficient { get; private set; }

    public CountObjectsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopObject shopItem) => Visit((dynamic)shopItem);

    public void Visit(ImmovablesItemObject immovablesItemObject)
    {

        var item = _persistentData.PlayerData.ImmovablesContainer.Find(x => x.ImmovablesObjects == immovablesItemObject.ObjectType);
        if (item == null)
        {
            Count = 0;
            return;
        }

        Count = item.Count;
        Profit = item.Profit();
        Coefficient = item.Coefficient;
    }

    public void Visit(IndustryItemObject industryItemObject)
    {

        var item = _persistentData.PlayerData.IndustrysContainer.Find(x => x.IndustrySubjects == industryItemObject.ObjectType);
        if (item == null)
        {
            Count = 0;
            return;
        }

        Count = item.Count;
        Profit = item.Profit();
        Coefficient = item.Coefficient;
    }
}