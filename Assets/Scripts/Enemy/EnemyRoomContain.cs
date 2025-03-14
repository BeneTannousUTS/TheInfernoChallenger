using UnityEngine;

public class EnemyRoomContain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            if (enemy != null && !enemy.IsFlipping) // Prevent multiple flips in quick succession
            {
                enemy.Flip();
                enemy.StartFlipCooldown();
            }
        }
    }
}