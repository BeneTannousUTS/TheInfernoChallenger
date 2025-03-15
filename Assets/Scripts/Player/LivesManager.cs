using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] Vector3 respawnPos = new Vector3(-7f, 0.5f, 0f);
    public int furthestLevel = 0;
    private static LivesManager instance;

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
            SceneManager.LoadScene(0);
            respawnPos = spawnPos;
        }
        else 
        {
            lives = 3;
            respawnPos = new Vector3(-7f, 0.5f, 0f);
            furthestLevel = 0;
            FindAnyObjectByType<UIManager>().LoseGame();
            FindAnyObjectByType<UIManager>().UpdateLives(0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject.FindWithTag("Player").transform.position = respawnPos;
        FindAnyObjectByType<UIManager>().UpdateLives(lives);
        if (respawnPos.y != 0.5f) {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().PlaceFlag(respawnPos);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().furthestLevelReached = furthestLevel;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().currentLevel = furthestLevel;
        }
    }

    public void Reset() 
    {
        lives = 3;
        respawnPos = new Vector3(-7f, 0.5f, 0f);
        furthestLevel = 0;
    }
}
