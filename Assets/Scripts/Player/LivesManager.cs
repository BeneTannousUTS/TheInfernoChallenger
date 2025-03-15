using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] Vector3 respawnPos = new Vector3(-7f, 0.5f, 0f);
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

    public void Respawn(Vector3 spawnPos) 
    {
        lives -= 1;
        if (lives != 0)
        {
            SceneManager.LoadScene(0);
            respawnPos = spawnPos;
        }
        else 
        {
            lives = 3;
            respawnPos = new Vector3(-7f, 0.5f, 0f);
            FindAnyObjectByType<UIManager>().LoseGame();
            FindAnyObjectByType<UIManager>().UpdateLives(0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject.FindWithTag("Player").transform.position = respawnPos;
        FindAnyObjectByType<UIManager>().UpdateLives(lives);
    }
}
