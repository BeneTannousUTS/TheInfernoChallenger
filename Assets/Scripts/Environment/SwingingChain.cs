using System.Collections;
using UnityEngine;

public class SwingingChain : MonoBehaviour
{
    [SerializeField]
    float swingVelocity; // must start as a +ve number
    float maxSwing = 45f;
    [SerializeField]
    GameObject ropeHold;
    bool isOnCooldown = false;
    
    void Update()
    {
        transform.Rotate(0,0,swingVelocity);

        float zRot = transform.rotation.eulerAngles.z > 180f ? transform.rotation.eulerAngles.z - 360: transform.rotation.eulerAngles.z;

        if (Mathf.Abs(maxSwing - zRot) < 2.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, maxSwing);
            swingVelocity *= -1;
            maxSwing *= -1;
        }
    }

    public void AttachPlayer(GameObject player)
    {
        if (!isOnCooldown)
        {
            Debug.Log("Attached Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.currentChain = this;
            playerMovement.rb.linearVelocityX = 0;
            playerMovement.rb.linearVelocityY = 0;
            playerMovement.rb.gravityScale = 0;
            player.transform.SetParent(ropeHold.transform);
            player.transform.localPosition = Vector3.zero;
            isOnCooldown = true;
        }
    }

    public void DeattachPlayer(GameObject player)
    {
        Debug.Log("Deattached Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        player.transform.SetParent(null);
        player.transform.rotation = Quaternion.Euler(0,0,0);
        playerMovement.rb.linearVelocityX = playerMovement.moveSpeed * maxSwing / Mathf.Abs(maxSwing);
        playerMovement.rb.linearVelocityY = playerMovement.jumpSpeed;
        playerMovement.rb.gravityScale = playerMovement.baseGravityScale;
        playerMovement.currentChain = null;
        StartCoroutine(RopeCooldown());
    }

    IEnumerator RopeCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(0.35f);
        isOnCooldown = false;
    }
}
