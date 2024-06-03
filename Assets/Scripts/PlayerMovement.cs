using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;
    public float moveSpeed = 5.0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float timer = 0.0f;

    public TextMeshProUGUI activeBulletCount;


    public int startHP = 3;
    public static int HP { get; private set; }
    // hit invuln
    public float bulletCooldown;
    float bulletTimer;

    public float slowMotionFactor = 0.5f; // Factor by which time scale is slowed down
    public float slowMotionDuration = 2.0f; // Duration of slow motion in seconds

    private bool isSlowMotionActive = false;
    private float originalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Player shooting
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= fireRate)
        {
            Shoot();
            timer = 0.0f;
        }
        bulletTimer -= Time.deltaTime;
        activeBulletCount.text =  "Active bullets: " + ObjectPool.Instance.GetActiveInHierarchyCount();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlowMotionActive)
        {
            isSlowMotionActive = true;
            Time.timeScale = slowMotionFactor;
            Invoke("DeactivateSlowMotion", slowMotionDuration);
        }
    }

    void Shoot()
    {
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = ObjectPool.Instance.GetPooledObj();
        bullet.GetComponent<Bullet>().isEnemy = false;
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
        }
    }

    void DeactivateSlowMotion()
    {
        isSlowMotionActive = false;
        Time.timeScale = originalTimeScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            HP -= 1;
            // reset invuln period
            bulletTimer = bulletCooldown;
        }
    }
}
