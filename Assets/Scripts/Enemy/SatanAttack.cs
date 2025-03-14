using System.Collections;
using UnityEngine;

public class SatanAttack : MonoBehaviour
{
    public GameObject fireBall;
    private bool attacking = false;
    private int satanHealth = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    IEnumerator AttackA()
    {
        Debug.Log("A");
        attacking = true;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        projectile = Instantiate(fireBall, transform.position + Vector3.left*2, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
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
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
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
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up*1.5f, transform.rotation);
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
        projectile = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile2 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        projectile3 = Instantiate(fireBall, transform.position + Vector3.left * 2 + Vector3.up * 1.5f, transform.rotation);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator Wait()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
    }

    public void takeDamage()
    {
        satanHealth -= 1;
        Debug.Log("Remaining health: " + satanHealth);
        if (satanHealth == 0)
        {
            FindAnyObjectByType<UIManager>().WinGame();
            Destroy(gameObject);
        }
    }
}
