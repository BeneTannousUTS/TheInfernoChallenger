using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] Vector3 respawnPos = new Vector3(-7f, 0.5f, 0f);
    public int furthestLevel = 0;
    private static LivesManager instance;
    private float storedTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Respawn(Vector3 spawnPos, int level) 
    {
        lives -= 1;
        furthestLevel = level;
        if (lives != 0)
        {
            storedTime = FindAnyObjectByType<UIManager>().GetTimer();
            SceneManager.LoadScene(1);
            respawnPos = spawnPos;
        }
        else 
        {
            lives = 3;
            respawnPos = new Vector3(-7f, 0.5f, 0f);
            furthestLevel = 0;
            storedTime = 0f;
            FindAnyObjectByType<UIManager>().LoseGame();
            FindAnyObjectByType<UIManager>().UpdateLives(0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (GameObject.FindWithTag("Player")) {
            GameObject.FindWithTag("Player").transform.position = respawnPos;
        }
        if (FindAnyObjectByType<UIManager>()) {
            FindAnyObjectByType<UIManager>().UpdateLives(lives);
            FindAnyObjectByType<UIManager>().SetTimer(storedTime);
        }
        if (respawnPos.y != 0.5f) {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().PlaceFlag(respawnPos);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().furthestLevelReached = furthestLevel;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().currentLevel = furthestLevel;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().respawnPoint = respawnPos;
        }
    }

    public void Reset() 
    {
        lives = 3;
        respawnPos = new Vector3(-7f, 0.5f, 0f);
        furthestLevel = 0;
        storedTime = 0f;
    }
}
