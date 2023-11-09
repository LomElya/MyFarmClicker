using System;
using Newtonsoft.Json;

public class BonusData
{
    public ListBonus Bonus { get; private set; }

    private int _count;


    public BonusData(ListBonus bonus)
    {
        Bonus = bonus;
        AddBonus();
    }


    [JsonConstructor]
    public BonusData(int count, ListBonus bonus)
    {
        Count = count;

        Bonus = bonus;
    }

    public int Count
    {
        get => _count;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _count = value;
        }
    }
    public int AddBonus() => ++Count;
}