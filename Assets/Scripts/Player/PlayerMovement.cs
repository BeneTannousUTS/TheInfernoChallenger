using UnityEngine;
using System.Collections;

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
    public float usedJumpSpeed; 
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
    private int fireCount = 2;
    private bool canFire = true;
    private bool fireWait = false;
    public ParticleSystem particles;
    private bool facingLeft = false;

    bool waterLevel;
    bool satanLevel;

    [SerializeField] private Vector3 respawnPoint = new Vector3(-7f, 0.5f, 0f);
    [SerializeField] private int furthestLevelReached = 0;
    [SerializeField] private int currentLevel = 0;

    public bool paused = false;
    public bool isDead = false;

    void Start()
    {
        Application.targetFrameRate = 60; // JUST FOR TESTING THIS CAN BE REMOVED LATER
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        float characterHeightFromCenterToGround = 0.6f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,Vector2.down,characterHeightFromCenterToGround,IgnoreCameraSnap);

        if (raycastHit2D)
        {
            coyoteTimer = coyoteTime;
        }
        else if (waterLevel)
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

        //Debug.Log("jumptime: " + jumpBufferTimer + "|| coyotime: " + coyoteTimer);

        if (jumpBufferTimer > 0 && coyoteTimer > 0 && !currentChain && !onLadder)
        {
            anim.SetTrigger("Jump");
            rb.linearVelocityY = usedJumpSpeed;
            jumpBufferTimer = 0;
            coyoteTimer = 0;
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

        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > 1 && fireCount > 0)
            {
                fireTimer = 0;
                canFire = true;
                particles.Play();
            }
        }
        if (fireWait)
        {
            turnTimer += Time.deltaTime;
            if (turnTimer > 4)
            {
                turnTimer = 0;
                fireCount = 2;
            }
        }

        if (paused) 
        {
            rb.linearVelocityX = 0f;
            rb.linearVelocityY = 0f;
        }
    }

    void FireBall()
    {
        if (canFire)
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
                canFire = false;
                fireCount -= 1;
                fireTimer = 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("FirePit") && collision.gameObject.GetComponent<FirePit>().canDamage && !isDead) 
        {
            isDead = true;
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("ChainRope") && !currentChain)
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<SwingingChain>().AttachPlayer(gameObject);
        }
        else if (collider.gameObject.CompareTag("Enemy") && !isDead) //Add enemy tag and any other obstacle tags 
        {
            isDead = true;
            Die();
        }
        else if (collider.gameObject.CompareTag("CameraSnapPos")) {
            mainCamera.transform.position = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y, -10f);
            currentLevel = collider.gameObject.GetComponent<CheckpointScript>().level;
            waterLevel = collider.gameObject.GetComponent<CheckpointScript>().waterLevel;
            satanLevel = collider.gameObject.GetComponent<CheckpointScript>().satanLevel;
            if (waterLevel) 
            {
                rb.gravityScale = 0.5f*baseGravityScale;
                usedJumpSpeed = 0.7f*jumpSpeed;
            }
            else 
            {
                rb.gravityScale = baseGravityScale;
                usedJumpSpeed = jumpSpeed;
            }
            if (satanLevel)
            {
                StartCoroutine(StartBoss());
            }
            FindAnyObjectByType<UIManager>().UpdateLevel(currentLevel);
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

    public void Die()
    {
        // Call to livesManager
        if (!paused) 
        {
            FindAnyObjectByType<LivesManager>().Respawn(respawnPoint);
        }
    }

    IEnumerator StartBoss() 
    {
        GameObject.FindWithTag("BackWall").GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(2f);
        GameObject.FindWithTag("Satan").GetComponent<SatanAttack>().active = true;
    }
}