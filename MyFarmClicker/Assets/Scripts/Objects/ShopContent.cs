using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<ImmovablesItemObject> _immovablesItemObjects;
    [SerializeField] private List<IndustryItemObject> _industryItemObjects;

    public IEnumerable<ImmovablesItemObject> ImmovablesItemObjects => _immovablesItemObjects;
    public IEnumerable<IndustryItemObject> IndustryItemObjects => _industryItemObjects;

    private void OnValidate()
    {
        var immovablesItemDuplicates = _immovablesItemObjects.GroupBy(item => item.ObjectType)
        .Where(array => array.Count() > 1);

        if (immovablesItemDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_immovablesItemObjects));

        var industryItemDuplicates = _industryItemObjects.GroupBy(item => item.ObjectType)
    .Where(array => array.Count() > 1);

        if (industryItemDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_industryItemObjects));
    }
}
