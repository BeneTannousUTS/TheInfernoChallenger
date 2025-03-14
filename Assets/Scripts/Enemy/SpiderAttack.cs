using System.Collections;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public float smallBounceForce = 2f;
    public float leapForce = 5f;
    public float pauseTime = 0.5f;
    public float distanceToAttack = 5f;

    private Rigidbody2D rb;
    public GameObject player;
    public bool isAttacking = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!isAttacking && Vector2.Distance(transform.position, player.transform.position) <= distanceToAttack && rb.linearVelocityY < 0.1 && rb.linearVelocityY > -0.1)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        if (!isAttacking)
        {
            GetComponent<EnemyBase>().SetIsAttacking(true);
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);

            // First small bounce
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, smallBounceForce);
            yield return new WaitForSeconds(0.4f);

            // Second small bounce
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, smallBounceForce);
            yield return new WaitForSeconds(0.4f);

            // Pause before leaping
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(pauseTime);

            // Leap towards player
            Vector2 direction = (player.transform.position - transform.position).normalized;
            direction.y += 1;
            rb.linearVelocity = direction * leapForce;

            yield return new WaitForSeconds(1.7f); // Wait before next attack

            GetComponent<EnemyBase>().SetIsAttacking(false);
            isAttacking = false;
        }
    }
}
