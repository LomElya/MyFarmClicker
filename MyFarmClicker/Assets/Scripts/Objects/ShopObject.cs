using UnityEngine;

public abstract class ShopObject : ScriptableObject
{
    [field: SerializeField] public GameObject Model { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField, Range(0, 9999999)] public int OppeningPrice { get; private set; }
    [field: SerializeField, Range(0, 9999999)] public int Price { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int CoinPerClick { get; private set; }
}
