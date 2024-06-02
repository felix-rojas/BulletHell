using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startHP = 3;
    public static int HP { get; private set; }

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
        if (other.tag == "Bullet")
        {
            Player.HP -= 1;
            // reset invuln period
            bulletTimer = bulletCooldown;
        }
    }
}
