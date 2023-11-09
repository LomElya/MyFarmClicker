using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _immovablesObjectButton;
    [SerializeField] private ShopCategoryButton _industryObjectButton;

    [SerializeField] private ModelsPanel _modelsPanel;

    [SerializeField] private ShopPanel _shopPanel;

    private IDataProvider _dataProvider;

    private ShopObjectView _previewedItem;

    private Wallet _wallet;

    private ObjectSelector _objectSelector;
    private ObjectUnlocker _objectUnlocker;
    private CountItemVisitor _countItemVisitor;
    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;
    private CountObjectsChecker _countObjectsChecker;

    private void OnEnable()
    {
        _immovablesObjectButton.Click += OnImmovablesObjectButtonClick;
        _industryObjectButton.Click += OnIndustryObjectButtonClick;

        _modelsPanel.ClickBuyButton += OnBuyButtonClick;
    }

    private void OnDisable()
    {
        _immovablesObjectButton.Click -= OnImmovablesObjectButtonClick;
        _industryObjectButton.Click -= OnIndustryObjectButtonClick;
        _shopPanel.ItemViewClicked -= OnItemViewClicked;

        _modelsPanel.ClickBuyButton -= OnBuyButtonClick;
    }

    public void Init(IDataProvider dataProvider, Wallet wallet)
    {
        _dataProvider = dataProvider;

        _wallet = wallet;

        _modelsPanel.Init(_wallet);

        _shopPanel.Init(_openObjectsChecker, _boughtObjectChecker, _countObjectsChecker);

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        OnImmovablesObjectButtonClick();
    }

    public void SetVisiter(ObjectSelector objectSelector, ObjectUnlocker objectUnlocker, CountItemVisitor countItemVisitor, OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker, CountObjectsChecker countObjectsChecker)
    {
        _objectSelector = objectSelector;
        _objectUnlocker = objectUnlocker;
        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;

        _countItemVisitor = countItemVisitor;
        _countObjectsChecker = countObjectsChecker;
    }

    private void OnItemViewClicked(ShopObjectView item)
    {
        _previewedItem = item;

        _modelsPanel.ShowModel(item, _openObjectsChecker, _boughtObjectChecker);
    }

    private void OnBuyButtonClick(int price, int value)
    {
        _countObjectsChecker.Visit(_previewedItem.Item);

        _openObjectsChecker.Visit(_previewedItem.Item);

        //Если открыто и IndustryItemObject, то добавить(изменить)
        if (_openObjectsChecker.IsOpened)
        {
            for (int i = 0; i < value; i++)
                _countItemVisitor.Visit(_previewedItem.Item);

            if (BuyItem(price))
                _previewedItem.SetCount(_countItemVisitor.Count);
        }
        else
        {
            if (BuyItem(price))
            {
                _objectUnlocker.Visit(_previewedItem.Item);
                _countItemVisitor.Visit(_previewedItem.Item);

                SelectSkin();

                _previewedItem.UnLock();
                _previewedItem.SetCount(_countItemVisitor.Count);
                _modelsPanel.ShowModel(_previewedItem, _openObjectsChecker, _boughtObjectChecker);
                _shopPanel.Sort();
            }
        }

        _dataProvider.Save();

    }

    private void OnImmovablesObjectButtonClick()
    {
        _immovablesObjectButton.Select();
        _industryObjectButton.UnSelect();
        _shopPanel.Show(_contentItems.ImmovablesItemObjects.Cast<ShopObject>());
    }

    private void OnIndustryObjectButtonClick()
    {
        _immovablesObjectButton.UnSelect();
        _industryObjectButton.Select();
        _shopPanel.Show(_contentItems.IndustryItemObjects.Cast<ShopObject>());
    }

    private void SelectSkin()
    {
        _objectSelector.Visit(_previewedItem.Item);
        _shopPanel.Select(_previewedItem);
    }

    private bool BuyItem(int price)
    {

        if (_wallet.IsEnough(price))
        {
            _wallet.Spend(price);

            return true;
        }

        return false;
    }
}
