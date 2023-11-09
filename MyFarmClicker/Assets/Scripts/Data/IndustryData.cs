using System;
using Newtonsoft.Json;

public class IndustryData : IDataItem
{
    public IndustrySubjects IndustrySubjects { get; private set; }
    private int _count;

    private float _сoefficient;


    public IndustryData(IndustrySubjects industrySubjects) => IndustrySubjects = industrySubjects;

    [JsonConstructor]
    public IndustryData(IndustrySubjects industrySubjects, int count, float сoefficient)
    {
        Count = count;

        Coefficient = сoefficient;

        IndustrySubjects = industrySubjects;
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