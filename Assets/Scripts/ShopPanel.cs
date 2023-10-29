using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopObjectView> ItemViewClicked;

    private List<ShopObjectView> _shopItems = new List<ShopObjectView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopObjectViewFactory _shopObjectViewFactory;

    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;

    public void Init(OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker)
    {
        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;
    }

    public void Show(IEnumerable<ShopObject> items)
    {
        Clear();

        foreach (ShopObject item in items)
        {
            ShopObjectView spawnedItem = _shopObjectViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;

            spawnedItem.UnSelect();
            spawnedItem.UnHighlight();

            _openObjectsChecker.Visit(spawnedItem.Item);

            if (_openObjectsChecker.IsOpened)
            {
                _boughtObjectChecker.Visit(spawnedItem.Item);

                if (_boughtObjectChecker.IsBought)
                {
                    spawnedItem.Select();
                    spawnedItem.Highlight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }

                spawnedItem.UnLock();
            }
            else
                spawnedItem.Lock();

            _shopItems.Add(spawnedItem);
        }
    }

    public void Select(ShopObjectView itemView)
    {
        foreach (var item in _shopItems)
            item.UnSelect();

        itemView.Select();
    }

    private void OnItemViewClick(ShopObjectView obj)
    {
        Highlight(obj);
        ItemViewClicked?.Invoke(obj);
    }

    private void Highlight(ShopObjectView shopObjectView)
    {
        foreach (var item in _shopItems)
            item.UnHighlight();

        shopObjectView.Highlight();
    }

    private void Clear()
    {
        foreach (ShopObjectView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
