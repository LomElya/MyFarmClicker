using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using UnityEngine;

public class PlayerData
{
    private ImmovablesObjects _boughtImmovablesObject;
    private IndustrySubjects _boughtIndustryItemObject;

    private List<ImmovableData> _immovablesContainer;
    private List<IndustryData> _industrysContainer;

    private List<ImmovablesObjects> _openImmovablesObjects;
    private List<IndustrySubjects> _openIndustryItemObjects;

    private List<BonusData> _bonusContainer;

    private int _money;

    public PlayerData()
    {
        _money = 10000;

        //Выбранные объекты сначана
        _boughtImmovablesObject = ImmovablesObjects.GardenBed;
        //_boughtIndustryItemObject = IndustrySubjects.Wheat;
        _boughtIndustryItemObject = IndustrySubjects.Chicken;

        //Открытые объекты сначала
        _openImmovablesObjects = new List<ImmovablesObjects>() { _boughtImmovablesObject };
        _openIndustryItemObjects = new List<IndustrySubjects>() { _boughtIndustryItemObject };

        _immovablesContainer = new List<ImmovableData>() { new ImmovableData(_boughtImmovablesObject) };
        _industrysContainer = new List<IndustryData>() { new IndustryData(_boughtIndustryItemObject) };

        ImmovableData immovable = _immovablesContainer.Find(item => item.ImmovablesObjects == _boughtImmovablesObject);
        immovable.AddItem();

        IndustryData industry = _industrysContainer.Find(item => item.IndustrySubjects == _boughtIndustryItemObject);
        industry.AddItem();

        _bonusContainer = new List<BonusData>();
    }

    [JsonConstructor]
    public PlayerData(int money, ImmovablesObjects selectImmovablesObject, IndustrySubjects selectIndustrySubject,
        List<ImmovablesObjects> openImmovablesObjects, List<IndustrySubjects> openIndustrySubjects, List<ImmovableData> immovablesContainer, List<IndustryData> industrysContainer, List<BonusData> bonusContainer)
    {
        Money = money;

        _boughtImmovablesObject = selectImmovablesObject;
        _boughtIndustryItemObject = selectIndustrySubject;

        _openImmovablesObjects = openImmovablesObjects;
        _openIndustryItemObjects = openIndustrySubjects;

        _immovablesContainer = immovablesContainer;
        _industrysContainer = industrysContainer;

        _bonusContainer = bonusContainer;
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

    public List<ImmovableData> ImmovablesContainer => _immovablesContainer;

    public List<IndustryData> IndustrysContainer => _industrysContainer;

    public void OpenImmovablesObject(ImmovablesObjects obj)
    {
        if (_openImmovablesObjects.Contains(obj))
            throw new ArgumentException(nameof(obj));

        _openImmovablesObjects.Add(obj);

        _immovablesContainer.Add(new ImmovableData(obj));
    }

    public void OpenIndustrySubject(IndustrySubjects obj)
    {
        if (_openIndustryItemObjects.Contains(obj))
            throw new ArgumentException(nameof(obj));

        _openIndustryItemObjects.Add(obj);

        _industrysContainer.Add(new IndustryData(obj));

    }

    public int CountImmovablesObject(ImmovablesObjects obj)
    {
        ImmovableData item = _immovablesContainer.Find(item => item.ImmovablesObjects == obj);

        if (!_immovablesContainer.Contains(item))
            throw new ArgumentException(nameof(item));

        return item.AddItem();
    }

    public int CountIndustrySubject(IndustrySubjects obj)
    {
        IndustryData item = _industrysContainer.Find(item => item.IndustrySubjects == obj);

        if (!_industrysContainer.Contains(item))
            throw new ArgumentException(nameof(item));

        return item.AddItem();
    }

    public void AddBonus(IndustryItemObject industryItemObject)
    {
        /* ListBonus bonus = industryItemObject.Bonus.ListBonus;

        BonusData item = _bonusContainer.Find(x => x.Bonus == bonus);

        if (!_bonusContainer.Contains(item))
            throw new ArgumentException(nameof(item));

        item.AddBonus(); */
    }
}



