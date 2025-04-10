using UnityEngine;

public class InventoryMananger : MonoBehaviour
{
    //type 0 => Ammo - 2 => Score - 1 => keys (0 => red, 1 => blue, 2 => yellow, 3 => empty)

    [Header("--- Keys --- ")]
    [SerializeField] private bool[] Keys;
    
    [Header("--- Power Up --- ")]
    [SerializeField] private bool _doubleJump;
    [SerializeField] private float _duration;
    [SerializeField] private float _TMPduration;

    [Header("--- Reference --- ")]
    [SerializeField] HealthSystem _HS;

    public void SetKey(byte subtype) 
    {
        Keys[subtype] = true;
    }

    public bool GetKey(byte subtype)
    {
        return Keys[subtype];
    }

    public void addTime(float quantityToAdd)
    {
        _HS.RestoreLife(quantityToAdd);
    }

    public void setDoubleJump(bool value)
    {
        _doubleJump = value;
        _TMPduration = _duration;
    }

    public bool getDoubleJump()
    {
        return _doubleJump;
    }

    private void Update()
    {
        if (getDoubleJump())
        {
            _TMPduration -= Time.deltaTime;
            if (_TMPduration <= 0)
            {
                setDoubleJump(false);
                _TMPduration = _duration;
            }
        }
    }
}
