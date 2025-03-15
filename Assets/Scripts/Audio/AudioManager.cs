using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    [SerializeField] private AudioClip buttonClickClip, menuMusic, gameMusic, bossMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            ChangeMusic("Menu");
        }
        else {
            ChangeMusic("Game");
        }
        //currentMusic = overworldMusic;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic(string track) {
        switch (track) {
            case "Menu":
                musicSource.clip = menuMusic;
            break;
            case "Game":
                musicSource.clip = gameMusic;
            break;
            case "Boss":
                musicSource.clip = bossMusic;
            break;
        }
        musicSource.Play();
    }

    public void TogglePlay() {
        if(musicSource.isPlaying) musicSource.Pause();
        else musicSource.Play();
    }

    public void PlaySound(AudioClip clip) {
        effectSource.PlayOneShot(clip);
    }

    public void PlaySound(string clip) {
        
    }

    public void PlayClickSound() {
        effectSource.PlayOneShot(buttonClickClip);
    }
}
