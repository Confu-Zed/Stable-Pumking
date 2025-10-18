using UnityEngine;
using UnityEngine.UI;

public class Pendulum : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public bool movingClockwise;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
    }
    public void ChangeDir()
    {
        if (rb.angularVelocity == Vector3.zero)
            movingClockwise = !movingClockwise;
    }
    public void Move()
    {
        ChangeDir();

        if (movingClockwise)
        {
            rb.angularVelocity = Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (!movingClockwise)
        {
            rb.angularVelocity = Vector3.back * moveSpeed * Time.deltaTime;
        }
    }
}
