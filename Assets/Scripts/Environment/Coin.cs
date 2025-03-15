using UnityEngine;

public class Coin : MonoBehaviour
{
    private UIManager manager;
    public AudioManager audioManager;
    [SerializeField] private AudioClip collectClip; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      	manager = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            audioManager.PlaySound(collectClip);
          	manager.UpdateScore(250);
          	Destroy(gameObject);
        }
    }
}
