using System.Collections;
using UnityEngine;

public class Area_Attack : MonoBehaviour
{
    [Header("Area Attack Options")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform areaSpawnPoint;
    [SerializeField] private int rows = 5;
    [SerializeField] private int cols = 5;
    [SerializeField] private float spacing = 2f;
    [SerializeField] private float activationInterval = 1.5f;
    [Header("Cube Options")]
    [SerializeField] private HealthSystem bossHealth;
    [SerializeField] private int minCubesToActivate = 3;
    [SerializeField] private int maxCubesToActivate = 10;


    private Damage_Cube[,] cubes;

    void Start()
    {
        cubes = new Damage_Cube[rows, cols];

        Vector3 startPos = areaSpawnPoint.position;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector3 spawnPos = startPos + new Vector3(x * spacing, 0, y * spacing);
                GameObject cube = Instantiate(cubePrefab, spawnPos, Quaternion.identity, areaSpawnPoint);
                cubes[x, y] = cube.GetComponent<Damage_Cube>();
            }
        }
    }

    public void TriggerAttack()
    {
        StartCoroutine(ActivateRandomCubes());
    }

    IEnumerator ActivateRandomCubes()
    {
        foreach (var cube in cubes)
            cube.Deactivate();

        yield return new WaitForSeconds(0.2f);

        // Calcolo dinamico in base alla salute
        float current = bossHealth.getLife();
        float max = bossHealth.GetType().GetField("MaxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(bossHealth) is float val ? val : 100f;
        float healthRatio = Mathf.Clamp01(current / max);

        // Meno vita = più cubi
        int numToActivate = Mathf.RoundToInt(Mathf.Lerp(maxCubesToActivate, minCubesToActivate, healthRatio));

        for (int i = 0; i < numToActivate; i++)
        {
            int x = Random.Range(0, rows);
            int y = Random.Range(0, cols);
            cubes[x, y].ActivateWithWarning();
        }

        yield return new WaitForSeconds(activationInterval);
    }
}
