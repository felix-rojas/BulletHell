using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject boss;
    public float spawnInterval = 1.0f;
    public float movementSpeed = 5.0f;
    public int max_enemies = 3;
    public int current_enemies = 0;
    private float lifetime = 0.0f;
    private float enemyLifetime = 30.0f;
    private bool stopSpawning = false;

    public GameObject[] activeEnemies = new GameObject[3];

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        lifetime += Time.deltaTime;
        if (!stopSpawning)
        {
            if (timer >= spawnInterval && current_enemies < max_enemies)
            {
                activeEnemies[current_enemies] = Instantiate(enemyPrefab, transform.position + new Vector3(current_enemies * 8, 0, 0), transform.rotation);
                current_enemies++;
                timer = 0.0f;
            }
        }
        if (lifetime >= enemyLifetime)
        {
            for (int i = 0; i < max_enemies; i++)
            {
                Destroy(activeEnemies[i]);
                stopSpawning = true;
            }
            activeEnemies[0] = Instantiate(boss, transform.position, transform.rotation);
            lifetime = 0.0f;
        }
        if (lifetime >= enemyLifetime && stopSpawning)
        {
            Destroy(activeEnemies[0]);
        }
    }

}
