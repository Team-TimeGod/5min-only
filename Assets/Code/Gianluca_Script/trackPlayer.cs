using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class trackPlayer : MonoBehaviour
{
    

    [Header("Player Data")]
    [SerializeField] private CharacterController _cc;
    [SerializeField] private Vector3 _trackedPositionGrounded;
    [SerializeField] private Vector3 _trackedPositionFly;

    [Header("DEBUG")]
    [SerializeField] bool doOnce;
    [SerializeField] bool _isGrounded;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_cc.isGrounded)
        {
            _trackedPositionGrounded = _cc.transform.position;
            doOnce = true;
        }
        else
        {
            StartCoroutine(Track());
        }
        _isGrounded = _cc.isGrounded;
    }

    IEnumerator Track()
    {
        if (doOnce)
        {
            doOnce = false;
            _trackedPositionFly = _cc.transform.position;
        }
        yield return new WaitForSeconds(0);
    }

    public Vector3 getGPos()
    {
        return _trackedPositionGrounded;
    }
    public Vector3 getFPos()
    {
        return _trackedPositionFly;
    }

}
