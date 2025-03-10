using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpSpeed;

    void Start()
    {
        Application.targetFrameRate = 60; // JUST FOR TESTING THIS CAN BE REMOVED LATER
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            rb.linearVelocityX = xInput * moveSpeed;
        }

        float characterHeightFromCenterToGround = 0.6f; // will need to be updated when changed from default sprite
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround);
        if (Input.GetKeyDown(KeyCode.Space) && raycastHit2D)
        {
            rb.linearVelocityY = jumpSpeed;
        }
    }
}