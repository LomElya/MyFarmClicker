public interface IShopItemVisitor
{
    void Visit(ShopObject shopItem);
    void Visit(ImmovablesItemObject immovablesItemObject);
    void Visit(IndustryItemObject industryItemObject);
}
