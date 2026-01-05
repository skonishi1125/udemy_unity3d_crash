using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement data")]
    public float moveSpeed;
    public float rotationSpeed;

    private float verticalInput;
    private float horizontalInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // w = 1, s = -1...
        verticalInput = Input.GetAxis("Vertical"); // z
        horizontalInput = Input.GetAxis("Horizontal"); // x

        // back move adjust
        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        rb.velocity = movement;

        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }
}
