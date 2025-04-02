using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [Header("Camera Ref.")]
    [SerializeField] Transform _camera;
    [SerializeField] Transform _target;

    [Header("SetUp")]
    [SerializeField] float _speed;
    [SerializeField] float _distanceX;
    [SerializeField] float _distanceY;
    [SerializeField] float _distanceZ;

    private void Update()
    {
        float targetX = _target.position.x - _distanceX;
        float targetY = _target.position.y - _distanceY;
        float targetZ = _target.position.z - _distanceZ;
        _camera.position = new Vector3(Mathf.Lerp(_camera.position.x, targetX, _speed*Time.unscaledDeltaTime), Mathf.Lerp(_camera.position.y, targetY, _speed * Time.unscaledDeltaTime), Mathf.Lerp(_camera.position.z, targetZ, _speed * Time.unscaledDeltaTime));
    }


}
