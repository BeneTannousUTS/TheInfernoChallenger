using Unity.VisualScripting;
using UnityEngine;

public class EnemyRoomContain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag($"Enemy"))
        {
            collision.GetComponent<EnemyMovement>().Flip();
        }
    }
}
