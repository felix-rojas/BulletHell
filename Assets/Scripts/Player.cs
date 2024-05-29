using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startHP = 3;
    public int HP;
    // hit invuln
    public float bulletCooldown;
    float bulletTimer;
    void Start()
    {
        HP = startHP;
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && bulletTimer <= 0)
        {
            HP -= 1;
            bulletTimer = bulletCooldown;
        }
    }
}
