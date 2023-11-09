using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameObjectView : MonoBehaviour, IPointerClickHandler
{
    public event Action<GameObjectView> Click;

    [SerializeField] private IntValueView _countView;

    [SerializeField] private TMP_Text _typeText;

    public ShopObject Item { get; private set; }

    public string Name => Item.Name;

    public GameObject Model => Item.Model;

    public Sprite Image => Item.Image;

    public void Init(ShopObject item)
    {
        Item = item;

        _typeText.text = Name;
    }

    //public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        Click?.Invoke(this);
    }

    public void SetCount(int count) => _countView.Show(count);
}