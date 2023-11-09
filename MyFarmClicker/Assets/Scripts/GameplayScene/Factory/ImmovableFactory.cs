using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ImmovableFactory", menuName = "GameplayExample/ImmovableFactory")]
public class ImmovableFactory : ScriptableObject
{
    [SerializeField] private List<ImmovableConfig> _immovableConfigs;

    public List<ImmovableConfig> ImmovableConfigs => _immovableConfigs;

    public Immovable Get(ImmovablesObjects immovablesObjects, Transform spawnPosition)
    {
        var item = _immovableConfigs.FirstOrDefault(x => x.ObjectType == immovablesObjects);

        Immovable newImmovable = Instantiate(item.ImmovablePrefab, spawnPosition);

        newImmovable.Init(item.ImmovablesItemObject);

        return newImmovable;
    }
}