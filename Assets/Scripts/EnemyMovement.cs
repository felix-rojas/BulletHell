using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    public float radius = 5.0f; // Radius of the circular path
    public float speed = 2.0f; // Speed of movement
    private Vector3 centerPosition; // Center position of the circle
    private float currentAngle = 0.0f; // Current angle on the circle
    private readonly System.Random rnd = new();
    private bool invert_axis = false;
    void Start()
    {
        centerPosition = transform.position; // Initial position is the center
        if (rnd.Next()%2 == 0)
        {
            invert_axis = true;
        }
    }

    void Update()
    {
        // Update the current angle based on time and speed
        currentAngle += speed * Time.deltaTime;
        Vector3 newPosition;
        // Calculate the new position on the circle based on the current angle
        float x = centerPosition.x + Mathf.Cos(currentAngle) * radius;
        float y = centerPosition.y + Mathf.Sin(currentAngle) * radius;
        if (invert_axis)
        {
            newPosition = new Vector3(-x, -y, centerPosition.z);
        }
        else
        {
            newPosition = new Vector3(x, y, centerPosition.z);
        }

        // Move the enemy to the new position
        transform.position = newPosition;
    }
}
