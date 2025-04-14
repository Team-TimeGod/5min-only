using UnityEngine;

public class Boss_PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool spawned = false;

    void Update()
    {
        if (!spawned && boss == null)
        {
            SpawnPlatform();
            spawned = true;
        }
    }

    void SpawnPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);

        Platform_Elevator elevator = platform.GetComponent<Platform_Elevator>();
        if (elevator != null)
        {
            elevator.StartElevator();
        }
    }
}
