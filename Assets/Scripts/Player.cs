using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public Collider self;

    public TextMeshProUGUI healthDisplay;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthDisplay.text = $"Health left: {currentHealth}";
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
        SceneManager.LoadScene("SampleScene");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
}