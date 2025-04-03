using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public Transform spawnPoint;
    public float spawnInterval = 1.0f;
    public float launchForce = 5f;
    public float lateralForce = 1.5f;
    public float torqueAmount = 10f;
    public GameObject bombPrefab; 

    private float timer;

void Update()
{
    if (!GameManager.Instance || !GameManager.Instance.gameRunning)
        return;

    timer += Time.deltaTime;

    if (timer >= GameManager.Instance.currentLevel.spawnInterval)
    {
        SpawnFruit();
        timer = 0f;
    }
}

void SpawnFruit()
{
    bool spawnBomb = Random.value < GameManager.Instance.currentLevel.bombChance;

    GameObject prefabToSpawn = spawnBomb
        ? bombPrefab
        : fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

    GameObject obj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

    if (obj.GetComponent<DestroyOnGround>() == null)
        obj.AddComponent<DestroyOnGround>();

    Rigidbody rb = obj.GetComponent<Rigidbody>();
    if (rb)
    {
        Vector3 force = Vector3.up * launchForce;
        force += new Vector3(Random.Range(-lateralForce, lateralForce), 0, Random.Range(-lateralForce, lateralForce));
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * torqueAmount, ForceMode.Impulse);
    }
}


}
