using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;

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
        _walletView.Init(_wallet);
    }

    private void InitShop()
    {
        ObjectSelector objectSelector = new ObjectSelector(_persistentData);
        ObjectUnlocker objectUnlocker = new ObjectUnlocker(_persistentData);
        CountItemVisitor countItemVisitor = new CountItemVisitor(_persistentData);
        OpenObjectsChecker openObjectsChecker = new OpenObjectsChecker(_persistentData);
        BoughtObjectChecker boughtObjectChecker = new BoughtObjectChecker(_persistentData);

        CountObjectsChecker countObjectsChecker = new CountObjectsChecker(_persistentData);

        _shop.SetVisiter(objectSelector, objectUnlocker, countItemVisitor, openObjectsChecker, boughtObjectChecker, countObjectsChecker);
        _shop.Init(_dataProvider, _wallet);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.PlayerData = new PlayerData();
    }

}
