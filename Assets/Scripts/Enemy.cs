using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public delegate void EnemyDiedHandler(GameObject enemy);
    public event EnemyDiedHandler OnEnemyDied;
    public Collider self;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) 
        { 
            TakeDamage(1);
        }
    }
}
