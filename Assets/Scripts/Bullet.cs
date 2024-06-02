using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 position;
    public float speed;
    public float rotation;
    public float lifeSpan = 3.0f;
    public float lifeTimer = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,rotation);
        lifeTimer = lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * velocity);
        lifeTimer -= Time.deltaTime;
        // recycle bullet
        if (lifeTimer <= 0) { gameObject.SetActive(false); }
    }

    public void ResetTimer()
    {
        lifeTimer = lifeSpan;
    }
}
