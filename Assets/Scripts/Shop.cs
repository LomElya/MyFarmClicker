using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _immovablesObjectButton;
    [SerializeField] private ShopCategoryButton _industryObjectButton;

    //Создать отдельный компонент ObjectPanel
    [SerializeField] private BuyButton _buyButton;

    [SerializeField] private ShopPanel _shopPanel;

    private IDataProvider _dataProvider;

    private ShopObjectView _previewedItem;

    private Wallet _wallet;

    private ObjectSelector _objectSelector;
    private ObjectUnlocker _objectUnlocker;
    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;

    private void OnEnable()
    {
        _immovablesObjectButton.Click += OnImmovablesObjectButtonClick;
        _industryObjectButton.Click += OnIndustryObjectButtonClick;
        _shopPanel.ItemViewClicked += OnItemViewClicked;

        _buyButton.Click += OnBuyButtonClick;
    }

    private void OnDisable()
    {
        _immovablesObjectButton.Click -= OnImmovablesObjectButtonClick;
        _industryObjectButton.Click -= OnIndustryObjectButtonClick;
        _shopPanel.ItemViewClicked -= OnItemViewClicked;

        _buyButton.Click -= OnBuyButtonClick;
    }

    public void Init(IDataProvider dataProvider, Wallet wallet,
        ObjectSelector objectSelector, ObjectUnlocker objectUnlocker, OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker)
    {
        _wallet = wallet;
        _objectSelector = objectSelector;
        _objectUnlocker = objectUnlocker;
        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;

        _dataProvider = dataProvider;

        _shopPanel.Init(openObjectsChecker, boughtObjectChecker);

        OnImmovablesObjectButtonClick();
    }

    private void OnItemViewClicked(ShopObjectView item)
    {
        _previewedItem = item;

        _openObjectsChecker.Visit(_previewedItem.Item);

        //Событие, если куплен
        if (_openObjectsChecker.IsOpened)
        {
            _boughtObjectChecker.Visit(_previewedItem.Item);

            if (_boughtObjectChecker.IsBought)
            {
                //Событие, если выбран
                HideBuyButton();
                return;
            }
        }
        else
            ShowBuyButton(_previewedItem.Price);

    }

    private void OnBuyButtonClick()
    {
        if (_wallet.IsEnough(_previewedItem.Price))
        {
            _wallet.Spend(_previewedItem.Price);

            _objectUnlocker.Visit(_previewedItem.Item);

            //Добавить объект
            //...
            SelectSkin();

            _previewedItem.UnLock();

            //_dataProvider.Save();

        }
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

    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price);

        if (_wallet.IsEnough(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();
    }

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
}
