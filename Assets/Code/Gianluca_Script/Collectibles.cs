using UnityEngine;

public class Collectibles : MonoBehaviour
{
    
    [SerializeField] private InventoryMananger _inventoryManager;
    [SerializeField] private Type _type;
    [SerializeField] private byte subtype;
    [SerializeField] private float quantityToAdd;

    private enum Keys : byte
    {
        Left = 0,
        Right = 1
    }

    private enum Type 
    {
        Key = 0,
        ExtraTime = 1,
        PowerUP = 2
    }

    private void Awake()
    {
        _inventoryManager = GameObject.Find("Manager").GetComponent<InventoryMananger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (_type)
        {
            case Type.Key:
                _inventoryManager.SetKey(subtype);
                break;
            case Type.ExtraTime:
                _inventoryManager.addTime(quantityToAdd);
                break;
            case Type.PowerUP:
                _inventoryManager.setDoubleJump(true);
                break;
        }

        Destroy(this.gameObject);

        //Debug.Log("disattivazione key");
        //gameObject.SetActive(false);

    }

}
