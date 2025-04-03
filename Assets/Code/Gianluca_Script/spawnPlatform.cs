using UnityEngine;
using System.Collections;

public class spawnPlatform : MonoBehaviour
{
    [Header("Prefab To Clone")]
    [SerializeField] private GameObject _platform;

    [Header("SetUp")]
    [SerializeField] private float _gapTime;
    [SerializeField] private byte _spawnLimitIndex;

    [Header("DEBUG")]
    [SerializeField] private byte _index = 0;
    [SerializeField] private bool _canSpawn;

    private void Update()
    {
        if (_index < _spawnLimitIndex && _canSpawn)
        {
             StartCoroutine(Spawn());
        }
    }


    IEnumerator Spawn()
    {
        _canSpawn = false;
        _index++;
        yield return new WaitForSeconds(_gapTime);
        Instantiate(_platform, this.transform.position, Quaternion.identity);
        _canSpawn = true;
    }
}
