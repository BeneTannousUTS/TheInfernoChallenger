using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D col;
    Animator anim;
    public float baseGravityScale = 3f;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float jumpSpeed;
    public SwingingChain currentChain = null;
    float yDir = 0f;
    bool onLadder = false;

    void Start()
    {
        Application.targetFrameRate = 60; // JUST FOR TESTING THIS CAN BE REMOVED LATER
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float characterHeightFromCenterToGround = 0.6f; // will need to be updated when changed from default sprite
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround);

        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            if (!currentChain && !onLadder)
            {
                rb.linearVelocityX = xInput * moveSpeed;
            }
        }

        float yInput = Input.GetAxisRaw("Vertical");
        if (yInput == 1)
        {
            RaycastHit2D ladderCheckUp = Physics2D.Raycast(transform.position,Vector2.up,characterHeightFromCenterToGround);
            if (ladderCheckUp && ladderCheckUp.transform.gameObject.CompareTag("Ladder")) 
            {
                ladderCheckUp.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                yDir = 1f;
                rb.linearVelocityX = 0f;
                rb.gravityScale = 0f;
                onLadder = true;
            }
        }
        else if (yInput == -1)
        {
            RaycastHit2D ladderCheckDown = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround);
            if (ladderCheckDown && ladderCheckDown.transform.gameObject.CompareTag("Ladder"))
            {
                ladderCheckDown.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                yDir = -1f;
                rb.linearVelocityX = 0f;
                rb.gravityScale = 0f;
                onLadder = true;
            }
        }

        if (onLadder) {
            rb.linearVelocityY = yDir*moveSpeed;
        }

        
        if (!currentChain && !onLadder) 
        {
            anim.SetFloat("walkSpeed", xInput);
        } else
        {
            anim.SetFloat("walkSpeed", 0);
        }


        if (Input.GetKeyDown(KeyCode.Space) && raycastHit2D && !currentChain && !onLadder)
        {
            Debug.Log("Jump");
            anim.SetTrigger("Jump");
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

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ladder") && onLadder)
        {
            collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            onLadder = false;
            rb.gravityScale = baseGravityScale;
        }
    }
}