using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SatanAttack : MonoBehaviour
{
    public GameObject fireBall;
    private bool attacking = false;
    public bool active = false;
    private float satanHealth = 30f;
    public Slider slider;
    public Animator anim;
    public AudioManager audioManager;
    [SerializeField] private AudioClip fireClip, hurtClip, deathClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) 
        {
            if (!attacking)
            {
                int rand = Random.Range(0, 5);
                if (rand == 0)
                {
                    StartCoroutine(AttackA());
                }
                if (rand == 1)
                {
                    StartCoroutine(AttackB());
                }
                if (rand == 2)
                {
                    StartCoroutine(AttackC());
                }
                if (rand == 3)
                {
                    StartCoroutine(AttackD());
                }
                if (rand == 4)
                {
                    StartCoroutine(Wait());
                }
            }
        }
    }

    IEnumerator AttackA()
    {
        Debug.Log("A");
        attacking = true;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        audioManager.PlaySound(fireClip);
        projectile = Instantiate(fireBall, transform.position + Vector3.left*2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator AttackB()
    {
        Debug.Log("B");
        attacking = true;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        audioManager.PlaySound(fireClip);
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator AttackC()
    {
        Debug.Log("C");
        attacking = true;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        audioManager.PlaySound(fireClip);
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.down*0.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up*1f, transform.rotation);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator AttackD()
    {
        Debug.Log("C");
        attacking = true;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        audioManager.PlaySound(fireClip);
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1f, transform.rotation);
        yield return new WaitForSeconds(1f);
        audioManager.PlaySound(fireClip);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1f, transform.rotation);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator Wait()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
    }

    IEnumerator Flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void takeDamage(float damage)
    {
        satanHealth -= damage;
        slider.value = satanHealth / 30f;
        Debug.Log("Remaining health: " + satanHealth);
        audioManager.PlaySound(hurtClip);
        StartCoroutine(Flash());
        if (satanHealth <= 0)
        {
            anim.SetTrigger("isDead");
            audioManager.PlaySound(deathClip);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().hasWon = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() 
    {
        active = false;
        yield return new WaitForSeconds(3f);
        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDead == false) {
            FindAnyObjectByType<UIManager>().WinGame();
        }
        Destroy(gameObject);

    }
}
