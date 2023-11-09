using System;
using UnityEngine;

public class ModelsPanel : MonoBehaviour
{
    public event Action<int, int> ClickBuyButton;

    [field: SerializeField] public BuyButton BuyButton { get; private set; }
    [field: SerializeField] public QuantityCounterItem QuantityCounter { get; private set; }
    [field: SerializeField] public ObjectPlacement ObjectPlacement { get; private set; }

    private ShopObjectView _previewedItem;

    private Wallet _wallet;

    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;

    private void OnEnable()
    {
        BuyButton.Click += OnBuyButtonClick;
        QuantityCounter.Click += OnQualityCounterClick;
    }

    private void OnDisable()
    {
        BuyButton.Click -= OnBuyButtonClick;
        QuantityCounter.Click -= OnQualityCounterClick;
    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void ShowModel(ShopObjectView item, OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker)
    {
        _previewedItem = item;

        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;

        ObjectPlacement.InstantiateModel(_previewedItem.Model);

        _openObjectsChecker.Visit(_previewedItem.Item);

        if (_openObjectsChecker.IsOpened)
        {
            ShowBuyButton(_previewedItem.Price * QuantityCounter.Value);
            QuantityCounter.Show();

            _boughtObjectChecker.Visit(_previewedItem.Item);
            if (_boughtObjectChecker.IsBought)
            {
                //Событие, если куплен


                return;
            }
        }
        else
        {
            ShowBuyButton(_previewedItem.OppeningPrice);
            QuantityCounter.Hide();
        }
    }

    private void OnBuyButtonClick()
    {
        int finalPrice;

        if (_openObjectsChecker.IsOpened)
            finalPrice = _previewedItem.Price * QuantityCounter.Value;
        else
            finalPrice = _previewedItem.OppeningPrice;

        ClickBuyButton?.Invoke(finalPrice, QuantityCounter.Value);
    }

    private void OnQualityCounterClick()
    {
        UpdatePriceBuyButton(_previewedItem.Price * QuantityCounter.Value);
    }

    private void ShowBuyButton(int price)
    {
        BuyButton.gameObject.SetActive(true);
        UpdatePriceBuyButton(price);
    }

    private void UpdatePriceBuyButton(int price)
    {
        BuyButton.UpdateText(price);

        if (_wallet.IsEnough(price))
            BuyButton.Unlock();
        else
            BuyButton.Lock();
    }

    private void HideBuyButton()
    {
        BuyButton.gameObject.SetActive(false);
        QuantityCounter.gameObject.SetActive(false);
    }
}