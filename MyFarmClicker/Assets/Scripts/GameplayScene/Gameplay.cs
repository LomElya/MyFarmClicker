using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private Immovable _immovable;
    private Industry _industry;

    private IDataProvider _dataProvider;

    private Wallet _wallet;

    private ObjectSelector _objectSelector;
    private ObjectUnlocker _objectUnlocker;
    private CountItemVisitor _countItemVisitor;
    private OpenObjectsChecker _openObjectsChecker;
    private BoughtObjectChecker _boughtObjectChecker;
    private CountObjectsChecker _countObjectsChecker;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        _immovable.Click -= OnObjectClicked;
        _industry.Click -= OnObjectClicked;
    }

    public void Init(Immovable immovable, Industry industry, IDataProvider dataProvider, Wallet wallet)
    {
        _dataProvider = dataProvider;

        _immovable = immovable;
        // _industry = industry;

        _wallet = wallet;

        _immovable.Click += OnObjectClicked;
        // _industry.Click += OnObjectClicked;

        _countObjectsChecker.Visit(_immovable.ImmovablesItemObject);
        _immovable.SetCount(_countObjectsChecker.Count);

        //_countObjectsChecker.Visit(_industry.IndustryItemObject);
        // _industry.SetCount(_countObjectsChecker.Count);
    }

    public void SetVisiter(ObjectSelector objectSelector, ObjectUnlocker objectUnlocker, CountItemVisitor countItemVisitor, OpenObjectsChecker openObjectsChecker, BoughtObjectChecker boughtObjectChecker, CountObjectsChecker countObjectsChecker)
    {
        _objectSelector = objectSelector;
        _objectUnlocker = objectUnlocker;
        _openObjectsChecker = openObjectsChecker;
        _boughtObjectChecker = boughtObjectChecker;

        _countItemVisitor = countItemVisitor;
        _countObjectsChecker = countObjectsChecker;
    }

    private void OnObjectClicked(GameItem gameItem)
    {
        float profit = gameItem.CoinPerClick * _countObjectsChecker.Coefficient;

        Debug.Log("Прибыль всего: " + profit);
        Debug.Log("Прибыль КЭФ: " + _countObjectsChecker.Coefficient);
        Debug.Log("Прибыль за 1: " + _countObjectsChecker.Profit);
        // _wallet.AddCoin(gameItem.AddCoins);

        //_dataProvider.Save();
    }
}
