using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int points = 200;
    
    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = false;
    public bool isAttacking = false;
    private UIManager uiManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
        movingRight = !movingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public void AwardPoints()
    {
        uiManager.UpdateScore(points);
    }
}
