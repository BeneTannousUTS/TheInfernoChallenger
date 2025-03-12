using UnityEngine;

public class FirePit : MonoBehaviour
{
    public bool canDamage;
    private float currentTime;
    public int changeTime;
    public Sprite[] sprites;
    public SpriteRenderer sprite;
    private bool increaseTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCheck();
    }

    private void TimeCheck()
    {
        if (increaseTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime > changeTime)
            {
                sprite.sprite = sprites[1];
                canDamage = false;
            }
            if (currentTime > 5)
            {
                increaseTime = false;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
            if (currentTime < changeTime)
            {
                sprite.sprite = sprites[0];
                canDamage = true;
            }
            if (currentTime < 1)
            {
                increaseTime = true;
            }
        }
    }
}
