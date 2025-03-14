using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int points = 200;
    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool movingRight = false;
    public bool isAttacking = false;
    private UIManager uiManager;
    
    public bool IsFlipping { get; private set; } = false;
    public float flipCooldown = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityX > 0)
        {
            sr.flipX = true;
        }else if (rb.linearVelocityX < 0)
        {
            sr.flipX = false;
        }
        if (!isAttacking)
        {
            Move();
        }
    }

    void Move()
    {
        // Move the enemy left or right
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.linearVelocity.y);
    }
    
    public void Flip()
    {
        rb.linearVelocityX *= -1;
        movingRight = !movingRight;
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public void AwardPoints()
    {
        uiManager.UpdateScore(points);
    }
    
    public void StartFlipCooldown()
    {
        if (!IsFlipping)
        {
            StartCoroutine(FlipCooldownRoutine());
        }
    }

    private IEnumerator FlipCooldownRoutine()
    {
        IsFlipping = true;
        yield return new WaitForSeconds(flipCooldown);
        IsFlipping = false;
    }
}
