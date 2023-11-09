using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopObjectView> ItemViewClicked;

    private List<ShopObjectView> _shopItems = new List<ShopObjectView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopObjectViewFactory _shopObjectViewFactory;

    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;
    private CountObjectsChecker _countObjectsChecker;

    public void Init(OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker, CountObjectsChecker countObjectsChecker)
    {
        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;
        _countObjectsChecker = countObjectsChecker;
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
                    spawnedItem.Highlight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }

                _countObjectsChecker.Visit(spawnedItem.Item);
                spawnedItem.Select();
                spawnedItem.UnLock();
                spawnedItem.SetCount(_countObjectsChecker.Count);
            }
            else
                spawnedItem.Lock();

            _shopItems.Add(spawnedItem);
        }

        Sort();
    }

    public void Select(ShopObjectView itemView)
    {
        //Для скрытия кнопки "куплено/Boughh" всех кроме выбранно
        /*   foreach (var item in _shopItems)
              item.UnSelect(); */

        itemView.Select();
    }

    public void Sort()
    {
        _shopItems = _shopItems
            .OrderBy(item => item.IsLock)
            .ThenByDescending(item => item.Price * -1)
            .ToList();

        for (int i = 0; i < _shopItems.Count; i++)
            _shopItems[i].transform.SetSiblingIndex(i);
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
