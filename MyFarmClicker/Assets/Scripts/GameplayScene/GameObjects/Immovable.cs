using System;
using Unity.VisualScripting;
using UnityEngine;

public class Immovable : GameItem
{
    private int _count;

    public ImmovablesItemObject ImmovablesItemObject { get; private set; }

    public ImmovablesObjects ObjectType => ImmovablesItemObject.ObjectType;
    public override int CoinPerClick => ImmovablesItemObject.CoinPerClick;

    public override void Init(ImmovablesItemObject immovablesItemObject)
    {
        ImmovablesItemObject = immovablesItemObject;

        Debug.Log("Инициализация недвижимости");
    }

    public override void SetCount(int count)
    {
        Count = count;
        Debug.Log(Count);
    }

    public override int Count
    {
        get => _count;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _count = value;
        }
    }
}
