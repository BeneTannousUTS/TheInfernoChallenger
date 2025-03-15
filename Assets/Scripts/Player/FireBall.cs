using UnityEngine;

public class FireBall : MonoBehaviour
{
    public bool facingRight;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            rb.linearVelocityX += 0.5f;
            if (rb.linearVelocityX > 10f) {
                rb.linearVelocityX = 10f;
            }
        }
        else
        {
            rb.linearVelocityX -= 0.5f;
            if (rb.linearVelocityX < -10f) {
                rb.linearVelocityX = -10f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("CameraSnapPos") && !collision.gameObject.CompareTag("FireBall") && !collision.gameObject.CompareTag("Coin") && !collision.gameObject.CompareTag("Ladder"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyBase>().AwardPoints();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Satan"))
            {
                collision.gameObject.GetComponent<SatanAttack>().takeDamage();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CameraSnapPos"))
        {
            Destroy(gameObject);
        }
    }
}
