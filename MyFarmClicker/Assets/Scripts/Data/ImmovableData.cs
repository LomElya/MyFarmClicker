using System;
using Newtonsoft.Json;

public class ImmovableData : IDataItem
{
    public ImmovablesObjects ImmovablesObjects { get; private set; }

    private int _count;

    private float _сoefficient;

    public ImmovableData(ImmovablesObjects immovablesObjects) => ImmovablesObjects = immovablesObjects;


    [JsonConstructor]
    public ImmovableData(int count, float сoefficient, ImmovablesObjects immovablesObjects)
    {
        Count = count;

        Coefficient = сoefficient;

        ImmovablesObjects = immovablesObjects;
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

    public float Coefficient
    {
        get => _сoefficient;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _сoefficient = value;
        }
    }

    public int AddItem()
    {
        Count++;
        _сoefficient += Profit();
        return Count;
    }

    public float Profit() => 1 - ((0.06f * Count) / (1 + 0.06f * Count));
}