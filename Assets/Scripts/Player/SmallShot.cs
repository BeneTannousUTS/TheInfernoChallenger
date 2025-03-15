using UnityEngine;

public class SmallShot : MonoBehaviour
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
            rb.linearVelocityX = 20f;
        }
        else
        {
            rb.linearVelocityX = -20f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("CameraSnapPos") && !collision.gameObject.CompareTag("FireBall") && !collision.gameObject.CompareTag("Coin") && !collision.gameObject.CompareTag("Ladder") && !collision.gameObject.name.Equals("ChainRope"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyBase>().AwardPoints();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Satan"))
            {
                collision.gameObject.GetComponent<SatanAttack>().takeDamage(0.3f);
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
