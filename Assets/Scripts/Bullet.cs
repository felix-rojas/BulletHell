using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velocity;
    public int pattern;
    public float speed;
    public float rotation;
    public float lifeSpan = 3.0f;
    public float lifeTimer;
    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        lifeTimer = lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * velocity);
        lifeTimer -= Time.deltaTime;
        // recycle bullet
        if (lifeTimer <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

    }

    private void OnEnable()
    {
        lifeTimer = lifeSpan;
        if (!isEnemy)
        {
            velocity = Quaternion.Euler(0, rotation, 0) * Vector3.forward;
        }
        else
        {
            velocity = Quaternion.Euler(0, rotation, 0) * Vector3.back;
        }
    }
    private void OnDisable()
    {
        lifeTimer = lifeSpan;
        isEnemy = false;
    }
}
