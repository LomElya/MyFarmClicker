using UnityEngine;

[System.Serializable]
public class IndustryConfig
{
    [SerializeField] private Industry _industryPrefab;
    [SerializeField] private IndustryItemObject _industryItemObject;

    public Industry IndustryPrefab => _industryPrefab;

    public IndustryItemObject IndustryItemObject => _industryItemObject;

    public IndustrySubjects ObjectType => _industryItemObject.ObjectType;
}