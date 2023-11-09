using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopObjectView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopObjectView> Click;

    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [SerializeField] private IntValueView _priceView;
    [SerializeField] private IntValueView _countView;

    [SerializeField] private TMP_Text _typeText;

    [SerializeField] private Image _boughtImage;

    private Image _backgroundImage;

    public ShopObject Item { get; private set; }

    public bool IsLock { get; private set; }

    public int Price => Item.Price;
    public int OppeningPrice => Item.OppeningPrice;

    public string Name => Item.Name;

    public GameObject Model => Item.Model;

    public Sprite Image => Item.Image;

    public void Init(ShopObject item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        Item = item;

        _contentImage.sprite = Image;
        _typeText.text = Name;

        _priceView.Show(Price);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Show(Price);
        _countView.Hide();
    }

    public void UnLock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Hide();
    }

    public void SetCount(int count) => _countView.Show(count);

    public void Select() => _boughtImage.gameObject.SetActive(true);
    public void UnSelect() => _boughtImage.gameObject.SetActive(false);

    public void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public void UnHighlight() => _backgroundImage.sprite = _standartBackground;
}
