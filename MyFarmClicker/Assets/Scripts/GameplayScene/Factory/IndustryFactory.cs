using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "IndustryFactory", menuName = "GameplayExample/IndustryFactory")]
public class IndustryFactory : ScriptableObject
{
    [SerializeField] private List<IndustryConfig> _industryConfigs;

    public List<IndustryConfig> IndustryConfigs => _industryConfigs;

    public Industry Get(IndustrySubjects industrySubjects, Transform spawnPosition)
    {
        var item = _industryConfigs.FirstOrDefault(x => x.ObjectType == industrySubjects);

        Industry newImmovable = Instantiate(item.IndustryPrefab, spawnPosition);

        newImmovable.Init(item.IndustryItemObject);

        return newImmovable;
    }
}