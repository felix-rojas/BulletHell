using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float circularRadius = 5.0f; // Radius of the circular path
    public float speed = 2.0f; // Speed of movement
    public float linearDistance = 10.0f; // Distance for linear movement
    public float zigzagAmplitude = 5.0f; // Amplitude of the zigzag movement
    public float zigzagFrequency = 1.0f; // Frequency of the zigzag movement
    public float patternSwitchTime = 10.0f; // Time in seconds to switch movement patterns

    private Vector3 startPosition; // Initial position of the boss
    private float currentAngle = 0.0f; // Current angle for circular movement
    private float currentPatternTime = 0.0f; // Timer to switch patterns
    private int currentPattern = 0; // Current movement pattern

    void Start()
    {
        startPosition = transform.position; // Initial position is the start position
    }

    void Update()
    {
        // Update the timer to switch patterns
        currentPatternTime += Time.deltaTime;
        if (currentPatternTime >= patternSwitchTime)
        {
            currentPattern = (currentPattern + 1) % 3; // Switch to the next pattern
            currentPatternTime = 0.0f; // Reset the timer
        }

        // Perform the movement based on the current pattern
        switch (currentPattern)
        {
            case 0:
                CircularMovement();
                break;
            case 1:
                LinearMovement();
                break;
            case 2:
                ZigzagMovement();
                break;
        }
    }

    void CircularMovement()
    {
        // Update the current angle based on time and speed
        currentAngle += speed * Time.deltaTime;

        // Calculate the new position on the circle based on the current angle
        float x = startPosition.x + Mathf.Cos(currentAngle) * circularRadius;
        float y = startPosition.y + Mathf.Sin(currentAngle) * circularRadius;
        Vector3 newPosition = new Vector3(x, y, startPosition.z);

        // Move the boss to the new position
        transform.position = newPosition;
    }

    void LinearMovement()
    {
        // Calculate the new position for linear movement back and forth
        float x = startPosition.x + Mathf.PingPong(Time.time * speed, linearDistance) - linearDistance / 2;
        float y = startPosition.y;
        Vector3 newPosition = new Vector3(x, y, startPosition.z);

        // Move the boss to the new position
        transform.position = newPosition;
    }

    void ZigzagMovement()
    {
        // Calculate the new position for zigzag movement
        float x = startPosition.x + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        float y = startPosition.y + Mathf.Cos(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector3 newPosition = new Vector3(x, y, startPosition.z);

        // Move the boss to the new position
        transform.position = newPosition;
    }
}
