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
    [SerializeField]
    float slowDownSpeed;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.2f;

    public SwingingChain currentChain = null;
    float yDir = 0f;
    bool onLadder = false;
    public Camera mainCamera;
    public LayerMask IgnoreCameraSnap;

    private float coyoteTimer = 0f;
    private float jumpBufferTimer = 0f;
    public GameObject fireBall;
    private float fireTimer = 5;
    private float turnTimer = 0;
    private bool canFire = true;
    private bool fireWait = false;
    private bool facingLeft = false;

    public bool isDead = false;

    [SerializeField] private Vector3 respawnPoint = new Vector3(-7f, 0.5f, 0f);
    [SerializeField] private int furthestLevelReached = 0;
    [SerializeField] private int currentLevel = 0;

    void Start()
    {
        Application.targetFrameRate = 60; // JUST FOR TESTING THIS CAN BE REMOVED LATER
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) {
            transform.position = respawnPoint;
            isDead = false;
        }
        
        float characterHeightFromCenterToGround = 0.6f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround,IgnoreCameraSnap);

        if (raycastHit2D)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            if (xInput > 0)
            {
                facingLeft = false;
            }
            else
            {
                facingLeft = true;
            }
            if (!currentChain && !onLadder)
            {
                rb.linearVelocityX = xInput * moveSpeed;
            }
        } else
        {
            int dir = (int) Mathf.Sign(rb.linearVelocityX);

            if (dir > 0)
            {
                rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX - slowDownSpeed, 0, moveSpeed);
            } else
            {
                rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX + slowDownSpeed, moveSpeed * -1, 0);
            }  
        }

        float yInput = Input.GetAxisRaw("Vertical");
        if (yInput == 1)
        {
            RaycastHit2D ladderCheckUp = Physics2D.Raycast(transform.position,Vector2.up,characterHeightFromCenterToGround*3,IgnoreCameraSnap);
            if (ladderCheckUp && ladderCheckUp.transform.gameObject.CompareTag("Ladder")) 
            {
                ladderCheckUp.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                yDir = 1f;
                onLadder = true;
            }
        }
        else if (yInput == -1)
        {
            RaycastHit2D ladderCheckDown = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround,IgnoreCameraSnap);
            if (ladderCheckDown && ladderCheckDown.transform.gameObject.CompareTag("Ladder"))
            {
                ladderCheckDown.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                yDir = -1f;
                onLadder = true;
            }
        }

        if (onLadder) {
            rb.linearVelocityY = yDir*moveSpeed;
            rb.linearVelocityX = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (!currentChain && !onLadder) 
        {
            anim.SetFloat("walkSpeed", xInput);
        } else
        {
            anim.SetFloat("walkSpeed", 0);
        }

        Debug.Log("jumptime: " + jumpBufferTimer + "|| coyotime: " + coyoteTimer);

        if (jumpBufferTimer > 0 && coyoteTimer > 0 && !currentChain && !onLadder)
        {
            anim.SetTrigger("Jump");
            rb.linearVelocityY = jumpSpeed;
            jumpBufferTimer = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY = rb.linearVelocityY * 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentChain)
        {
            currentChain.DeattachPlayer(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            FireBall();
        }

        if (fireWait)
        {
            fireTimer += Time.deltaTime;
            turnTimer += Time.deltaTime;
            if (turnTimer > 4)
            {
                fireTimer = 5;
                turnTimer = 0;
                canFire = true;
                fireWait = false;
            }
        }
    }

    void FireBall()
    {
        if (canFire)
        {
            if (fireTimer > 1)
            {
                GameObject projectile;
                if (facingLeft)
                {
                    projectile = Instantiate(fireBall, transform.position + Vector3.left, transform.rotation);
                    projectile.GetComponent<FireBall>().facingRight = false;
                }
                else
                {
                    projectile = Instantiate(fireBall, transform.position + Vector3.right, transform.rotation);
                    projectile.GetComponent<FireBall>().facingRight = true;
                }
                fireWait = true;
                if (fireTimer < 4)
                {
                    canFire = false;
                }
                fireTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("ChainRope") && !currentChain)
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<SwingingChain>().AttachPlayer(gameObject);
        }
        else if (collider.gameObject.CompareTag("CameraSnapPos")) {
            mainCamera.transform.position = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y, -10f);
            currentLevel = collider.gameObject.GetComponent<CheckpointScript>().level;
            if (collider.gameObject.GetComponent<CheckpointScript>().level > furthestLevelReached) 
            {
                furthestLevelReached = collider.gameObject.GetComponent<CheckpointScript>().level;
                respawnPoint = collider.gameObject.GetComponent<CheckpointScript>().checkpointWorldPos;
            }
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