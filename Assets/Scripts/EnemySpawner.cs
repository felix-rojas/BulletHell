using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject boss;
    public float spawnInterval = 1.0f;
    public float movementSpeed = 5.0f;
    public int max_enemies = 5;
    public int current_enemies = 0;
    private float lifetime = 0.0f;
    private float enemyLifetime = 30.0f;
    private bool stopSpawning = false;

    private List<GameObject> activeEnemies = new();

    public TextMeshProUGUI enemyCount;


    private float timer = 0.0f;


    private Vector3 GeneratePosition(int i)
    {

        // Golden ratio for cool math distribution
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;
            float t = (float)i / max_enemies;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = 8.0f * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = 8.0f * Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            
            return new Vector3(x, y, 0);
    }
    void Update()
    {
        enemyCount.text = "Enemies: " + activeEnemies.Count;
        timer += Time.deltaTime;
        lifetime += Time.deltaTime;
        if (!stopSpawning)
        {
            if (timer >= spawnInterval && activeEnemies.Count < max_enemies)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position + GeneratePosition(activeEnemies.Count), transform.rotation);
                activeEnemies.Add(enemy);
                timer = 0.0f;
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.OnEnemyDied += HandleEnemyDied;
                }
            }
        }
        if (lifetime >= enemyLifetime)
        {
            for (int i = 0; i < max_enemies; i++)
            {
                RemoveAllEnemies();
                stopSpawning = true;
            }
            Instantiate(boss, transform.position + new Vector3(0, 0, -8), transform.rotation);
            activeEnemies.Add(boss);
            lifetime = 0.0f;
        }
        if (lifetime >= enemyLifetime && stopSpawning)
        {
            RemoveAllEnemies();
        }
    }

    void HandleEnemyDied(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
    public void RemoveAllEnemies()
{
    foreach (var enemy in activeEnemies)
    {
        Destroy(enemy);
    }
    activeEnemies.Clear();
}
}
