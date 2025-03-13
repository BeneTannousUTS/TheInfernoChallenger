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
        }
        else
        {
            rb.linearVelocityX -= 0.5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("CameraSnapPos"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Insert Function to destroy enemy");
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
