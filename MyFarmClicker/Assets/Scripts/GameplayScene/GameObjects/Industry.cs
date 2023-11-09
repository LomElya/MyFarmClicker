using System;
using UnityEngine;

public class Industry : GameItem
{
    private int _count;

    public IndustryItemObject IndustryItemObject { get; private set; }

    public IndustrySubjects ObjectType => IndustryItemObject.ObjectType;
    public override int CoinPerClick => IndustryItemObject.CoinPerClick;

    public override void Init(IndustryItemObject industryItemObject)
    {
        IndustryItemObject = industryItemObject;
        Debug.Log("Инициализация Промышленности");
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
