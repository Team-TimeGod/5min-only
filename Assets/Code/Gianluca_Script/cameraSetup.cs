using UnityEngine;

public class cameraSetup : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private cameraManager _cameraManager;
    [Header("New Delta for Position")]
    [SerializeField] private float _newXGap;
    [SerializeField] private float _newYGap;
    [SerializeField] private float _newZGap;
    [Header("New Holder Rotation Degree")]
    [SerializeField] private float _newXDegreeHolder;
    [SerializeField] private float _newYDegreeHolder;
    [SerializeField] private float _newZDegreeHolder;
    [Header("New Camera Rotation Degree")]
    [SerializeField] private float _newXDegreeCamera;
    [SerializeField] private float _newYDegreeCamera;
    [SerializeField] private float _newZDegreeCamera;
    enum Type : byte 
    {
        SetupHolder = 0,
        SetupCamera = 1,
        SetupBoth = 2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_type)
            {
                case Type.SetupHolder:
                    _cameraManager.setDistance(_newXGap, _newYGap, _newZGap);
                    _cameraManager.setHolderRotation(_newXDegreeHolder, _newYDegreeHolder, _newZDegreeHolder);
                    break;
                case Type.SetupCamera:
                    _cameraManager.setCameraRotation(_newXDegreeCamera, _newYDegreeCamera, _newZDegreeCamera);
                    break;
                case Type.SetupBoth:
                    _cameraManager.setDistance(_newXGap, _newYGap, _newZGap);
                    _cameraManager.setHolderRotation(_newXDegreeHolder, _newYDegreeHolder, _newZDegreeHolder);
                    _cameraManager.setCameraRotation(_newXDegreeCamera, _newYDegreeCamera, _newZDegreeCamera);
                    break;
            }
        }
    }
}
