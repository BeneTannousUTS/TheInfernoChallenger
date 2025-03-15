using UnityEngine;

public class SatanFireBall : MonoBehaviour
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
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            rb.linearVelocityX -= 0.5f;
            if (rb.linearVelocityX < -10f) {
                rb.linearVelocityX = -10f;
            }
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Satan") && !collision.gameObject.CompareTag("CameraSnapPos") && !collision.gameObject.CompareTag("FireBall"))
        {
            if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerMovement>().isDead && !collision.gameObject.GetComponent<PlayerMovement>().hasWon)
            {
                collision.gameObject.GetComponent<PlayerMovement>().isDead = true;
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }
            Debug.Log(collision.gameObject.name);
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
