using UnityEngine;

public class GameplayBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] protected ImmovableFactory _immovableFactory;
    [SerializeField] protected IndustryFactory _industryFactory;

    [Space(15)]
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private WalletView _walletView;

    private Immovable _immovable1;
    private Industry _industry;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Wallet _wallet;

    private void Awake()
    {
        Init();
    }

    //Для загрузки уровня Gameplay через другие сцены
    public void Init()
    {
        InitData();

        InitWallet();

        InitGame();
    }

    private void InitData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);
        LoadDataOrInit();
    }

    private void InitWallet()
    {
        _wallet = new Wallet(_persistentData);
        _walletView.Init(_wallet);
    }

    private void DoSpawn(CountObjectsChecker countObjectsChecker)
    {
        _immovable1 = _immovableFactory.Get(_persistentData.PlayerData.BoughtImmovablesObject, _spawnPoint);

       // _industry = _industryFactory.Get(_persistentData.PlayerData.BoughtIndustrySubject, _spawnPoint);
    }

    private void InitGame()
    {
        ObjectSelector objectSelector = new ObjectSelector(_persistentData);
        ObjectUnlocker objectUnlocker = new ObjectUnlocker(_persistentData);
        CountItemVisitor countItemVisitor = new CountItemVisitor(_persistentData);
        OpenObjectsChecker openObjectsChecker = new OpenObjectsChecker(_persistentData);
        BoughtObjectChecker boughtObjectChecker = new BoughtObjectChecker(_persistentData);

        CountObjectsChecker countObjectsChecker = new CountObjectsChecker(_persistentData);

        DoSpawn(countObjectsChecker);

        _gameplay.SetVisiter(objectSelector, objectUnlocker, countItemVisitor, openObjectsChecker, boughtObjectChecker, countObjectsChecker);
        _gameplay.Init(_immovable1, _industry, _dataProvider, _wallet);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.PlayerData = new PlayerData();
    }

}
