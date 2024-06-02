using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableOjects/BulletBehavior", order = 1)]
public class BulletBehaviors : ScriptableObject
{
    public GameObject bulletPrefab;
    public Vector3 bulletVelocity;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isParent;
    public float spawnCooldown;
    public float bulletSpeed;
}
