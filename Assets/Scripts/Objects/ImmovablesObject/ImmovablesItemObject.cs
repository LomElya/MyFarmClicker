using UnityEngine;

[CreateAssetMenu(fileName = "ImmovablesItemObject", menuName = "Shop/ImmovablesItemObject")]
public class ImmovablesItemObject : ShopObject
{
    [field: SerializeField] public ImmovablesObjects ObjectType { get; private set; }
}

