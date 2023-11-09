using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private const string RenderLayer = "ObjectRender";

    [SerializeField] private Rotator _rotator;

    private GameObject _currentModel;

    public void InstantiateModel(GameObject model)
    {
        if (_currentModel != null)
            Destroy(_currentModel.gameObject);

        _rotator.ResetRotation();
        _currentModel = Instantiate(model, transform);

        Transform[] childrens = _currentModel.GetComponentsInChildren<Transform>();

        foreach (var item in childrens)
            item.gameObject.layer = LayerMask.NameToLayer(RenderLayer);

    }
}
