using UnityEngine;

[CreateAssetMenu(fileName = "IndustryItemObject", menuName = "Shop/IndustryItemObject")]
public class IndustryItemObject : ShopObject
{
    [field: SerializeField] public IndustrySubjects ObjectType { get; private set; }
}
