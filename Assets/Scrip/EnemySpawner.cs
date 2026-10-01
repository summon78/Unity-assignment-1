using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public int maxCount = 10;
    public float spawnInterval = 2f;
    public Vector3 spawnArea = new Vector3(20f, 0f, 20f);
    public bool showGizmos = true;
    public float fixedY = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            int currentCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxCount)
            {
                SpawnOne();
            }
        }
    }

    void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    SpawnOne();
        //}
    }

    void SpawnOne()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
            0f,
            Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f)
        );

        Vector3 finalPos = transform.position + randomPos;
        finalPos.y = fixedY;

        Enemy enemy = Instantiate(enemyPrefab, finalPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnArea.x, 0.1f, spawnArea.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 0.1f, spawnArea.z));
    }
}