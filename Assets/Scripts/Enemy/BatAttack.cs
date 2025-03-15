using System.Collections;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float pauseTime = 0.5f;
    public float distanceToAttack = 5f;
    public float swoopSpeed = 7f;
    public float returnSpeed = 4f;
    public float attackCooldown = 0.7f; // Time before next attack

    private Rigidbody2D rb;
    private Transform player;
    private bool isAttacking = false;
    private float originalHeight;
    private Vector2 swoopTarget;
    public AudioClip swoopAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalHeight = transform.position.y; // Store original height
    }

    void Update()
    {
        if (!isAttacking && Vector2.Distance(transform.position, player.position) <= distanceToAttack)
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
            yield return new WaitForSeconds(pauseTime); // Pause before swoop

            Vector2 startPos = transform.position;
            swoopTarget = player.position;
            
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound(swoopAudio);
            
            // Swoop towards player
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * swoopSpeed / Vector2.Distance(startPos, swoopTarget);
                float curveHeight = Mathf.Sin(t * Mathf.PI) * -1;
                transform.position = new Vector2(
                    Mathf.Lerp(startPos.x, swoopTarget.x, t), 
                    Mathf.Lerp(startPos.y, swoopTarget.y, t) + curveHeight
                );
                yield return null; // Wait for the next frame
            }

            // Return to original height (but keep new horizontal position)
            while (Mathf.Abs(transform.position.y - originalHeight) > 0.1f)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, originalHeight), returnSpeed * Time.deltaTime);
                yield return null;
            }
            
            yield return new WaitForSeconds(attackCooldown); // Cooldown before next attack

            // Reset attack state
            GetComponent<EnemyBase>().SetIsAttacking(false);
            isAttacking = false;
        }
    }
}
