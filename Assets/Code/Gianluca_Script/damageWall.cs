using UnityEngine;

public class damageWall : MonoBehaviour
{
    [Header("Referece")]
    [SerializeField] private HealthSystem _HS;
    [SerializeField] private trackPlayer _TP;
    [SerializeField] private Transform _Player;


    [Header("Wall Data")]
    [SerializeField] private float _damage;

    public void Awake()
    {
        _HS = GameObject.Find("Player").GetComponent<HealthSystem>();
        _TP = GameObject.Find("Player").GetComponent<trackPlayer>();
        _Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _HS.TakeDamage(_damage);
            if (_HS.getLife() > 0)
            {
                _Player.position = _TP.getGPos();
                Debug.Log("FAI BENE ");
            }
        }

    }
}
