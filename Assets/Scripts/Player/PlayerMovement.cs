using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 300f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            Debug.Log(xInput);
            rb.linearVelocityX = xInput * moveSpeed * Time.deltaTime;
        }
    }
}