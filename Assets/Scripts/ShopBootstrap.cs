using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Wallet _wallet;

    private void Awake()
    {
        InitData();

        InitWallet();

        InitShop();
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
    }

    private void InitShop()
    {
        ObjectSelector objectSelector = new ObjectSelector(_persistentData);
        ObjectUnlocker objectUnlocker = new ObjectUnlocker(_persistentData);
        OpenObjectsChecker openObjectsChecker = new OpenObjectsChecker(_persistentData);
        BoughtObjectChecker boughtObjectChecker = new BoughtObjectChecker(_persistentData);

        _shop.Init(_dataProvider, _wallet, objectSelector, objectUnlocker, openObjectsChecker, boughtObjectChecker);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.PlayerData = new PlayerData();
    }

}
