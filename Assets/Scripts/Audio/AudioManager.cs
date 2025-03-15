using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentMusic = overworldMusic;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic(string track) {

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
}
