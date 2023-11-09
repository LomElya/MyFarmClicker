using UnityEngine;

[System.Serializable]
public class ImmovableConfig
{
    [SerializeField] private Immovable _immovablePrefab;
    [SerializeField] private ImmovablesItemObject _immovablesItemObject;

    public Immovable ImmovablePrefab => _immovablePrefab;

    public ImmovablesItemObject ImmovablesItemObject => _immovablesItemObject;

    public ImmovablesObjects ObjectType => _immovablesItemObject.ObjectType;
}