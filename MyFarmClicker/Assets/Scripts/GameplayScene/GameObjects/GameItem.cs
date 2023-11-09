using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameItem : MonoBehaviour, IPointerClickHandler
{
    public event Action<GameItem> Click;

    public virtual int CoinPerClick => _shopObject.CoinPerClick;

    private ShopObject _shopObject;

    public void Init(ShopObject shopObject)
    {
        _shopObject = shopObject;
        Init((dynamic)shopObject);
    }

    public virtual void Init(ImmovablesItemObject immovablesItemObject)
    {

    }
    public virtual void Init(IndustryItemObject industryItemObject)
    {

    }

    public abstract int Count { get; set; }

    public abstract void SetCount(int count);

    public virtual void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
}
