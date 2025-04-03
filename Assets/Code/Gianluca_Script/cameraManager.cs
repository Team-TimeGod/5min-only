using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [Header("Camera Ref.")]
    [SerializeField] Transform _camera;
    [SerializeField] Transform _holder;
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
        _holder.position = new Vector3(Mathf.Lerp(_holder.position.x, targetX, _speed*Time.unscaledDeltaTime), Mathf.Lerp(_holder.position.y, targetY, _speed * Time.unscaledDeltaTime), Mathf.Lerp(_holder.position.z, targetZ, _speed * Time.unscaledDeltaTime));
    }

    public void setDistance(float newX, float newY, float newZ)
    {
        _distanceX = newX;
        _distanceY = newY;
        _distanceZ = newZ;
    }

    public void setCameraRotation(float xDegree, float yDegree, float zDegree)
    {
        _camera.rotation = Quaternion.Euler(xDegree, yDegree, zDegree);
    }
    public void setHolderRotation(float xDegree, float yDegree, float zDegree)
    {
        _holder.rotation = Quaternion.Euler(xDegree, yDegree, zDegree);
    }


}
