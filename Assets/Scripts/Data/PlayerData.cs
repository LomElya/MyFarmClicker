using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    
    private ImmovablesObjects _boughtImmovablesObject;
    private IndustrySubjects _boughtIndustryItemObject;

    private List<ImmovablesObjects> _openImmovablesObjects;
    private List<IndustrySubjects> _openIndustryItemObjects;

    private int _money;

    public PlayerData()
    {
        _money = 10000;

       /*  _boughtImmovablesObject = ImmovablesObjects.GardenBed;
        _boughtIndustryItemObject = IndustrySubjects.Wheat; */

        _openImmovablesObjects = new List<ImmovablesObjects>() { _boughtImmovablesObject };
        _openIndustryItemObjects = new List<IndustrySubjects>() { _boughtIndustryItemObject };
    }

    [JsonConstructor]
    public PlayerData(int money, ImmovablesObjects selectImmovablesObject, IndustrySubjects selectIndustrySubject,
        List<ImmovablesObjects> openImmovablesObjects, List<IndustrySubjects> openIndustrySubjects)
    {
        Money = money;

        _boughtImmovablesObject = selectImmovablesObject;
        _boughtIndustryItemObject = selectIndustrySubject;

        _openImmovablesObjects = openImmovablesObjects;
        _openIndustryItemObjects = openIndustrySubjects;
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public ImmovablesObjects BoughtImmovablesObject
    {
        get => _boughtImmovablesObject;

        set
        {
            if (_openImmovablesObjects.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _boughtImmovablesObject = value;
        }
    }

    public IndustrySubjects BoughtIndustrySubject
    {
        get => _boughtIndustryItemObject;

        set
        {
            if (_openIndustryItemObjects.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _boughtIndustryItemObject = value;
        }
    }

    public IEnumerable<ImmovablesObjects> OpenImmovablesObjects => _openImmovablesObjects;

    public IEnumerable<IndustrySubjects> OpenIndustrySubjects => _openIndustryItemObjects;

    public void OpenImmovablesObject(ImmovablesObjects obj)
    {
        if (_openImmovablesObjects.Contains(obj))
            throw new ArgumentException(nameof(obj));

        _openImmovablesObjects.Add(obj);
    }

    public void OpenIndustrySubject(IndustrySubjects obj)
    {
        if (_openIndustryItemObjects.Contains(obj))
            throw new ArgumentException(nameof(obj));

        _openIndustryItemObjects.Add(obj);
    }
}
