using System.Collections;
using UnityEngine;

public class PirahnaAttack : MonoBehaviour
{
    public float swimForce = 10f;
    public float pauseTime = 0.5f;
    public float distanceToAttack = 5f;

    private Rigidbody2D rb;
    private Transform player;
    public bool isAttacking = false;
    private Vector2 swimTarget;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isAttacking && Vector2.Distance(transform.position, player.transform.position) <= distanceToAttack)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        if (!isAttacking)
        {
            Debug.Log("Attacking");
            GetComponent<EnemyBase>().SetIsAttacking(true);
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);

            Vector2 attackPosition = (player.transform.position - transform.position).normalized;
            
            transform.localScale = new Vector3(transform.localScale.x/2, transform.localScale.y, transform.localScale.z);
            yield return new WaitForSeconds(0.5f);
            
            Vector2 startPos = transform.position;
            swimTarget = player.transform.position;
            
            transform.localScale = new Vector3(1, 1, 1);
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * swimForce / Vector2.Distance(startPos, swimTarget);
                transform.position = new Vector2(
                    Mathf.Lerp(startPos.x, swimTarget.x, t), 
                    Mathf.Lerp(startPos.y, swimTarget.y, t)
                );
                yield return null; // Wait for the next frame
            }

            yield return new WaitForSeconds(1.7f); // Wait before next attack

            GetComponent<EnemyBase>().SetIsAttacking(false);
            isAttacking = false;
        }
    }
}
