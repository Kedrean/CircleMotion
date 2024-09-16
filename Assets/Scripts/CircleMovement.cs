using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float radius = 2f; // Radius of the circular path
    public float speed = 5f; // Speed of movement along the path
    private float angle = 0f; // Current angle in radians
    private float direction = -1f; // -1 for clockwise, 1 for counterclockwise

    void Update()
    {
        // Check for input to toggle direction
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            direction *= -1f;
        }

        // Calculate the new angle based on speed and direction
        angle += speed * direction * Time.deltaTime;

        // Calculate the new position
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Apply the new position to the circle
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
