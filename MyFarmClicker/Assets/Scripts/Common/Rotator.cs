using UnityEngine;

/// <summary>
/// Вращение 3D модели(в будущем)
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _rotateSpeed;

    private float _currentRotation = 0;

    private void Update()
    {
        _currentRotation -= _rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
    }

    public void ResetRotation() => _currentRotation = 180;
}
