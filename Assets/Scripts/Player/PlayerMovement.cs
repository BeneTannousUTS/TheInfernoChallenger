using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float baseGravityScale = 3f;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float jumpSpeed;
    public SwingingChain currentChain = null;

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
            if (!currentChain)
            {
                rb.linearVelocityX = xInput * moveSpeed;
            }
        }

        float characterHeightFromCenterToGround = 0.6f; // will need to be updated when changed from default sprite
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround);
        if (Input.GetKeyDown(KeyCode.Space) && raycastHit2D && !currentChain)
        {
            Debug.Log("Jump");
            rb.linearVelocityY = jumpSpeed;
        } else if (Input.GetKeyDown(KeyCode.Space) && currentChain)
        {
            Debug.Log("Release Rope");
            currentChain.DeattachPlayer(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("ChainRope") && !currentChain)
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<SwingingChain>().AttachPlayer(gameObject);
        }
    }
}