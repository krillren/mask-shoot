using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton

    [Header("Audio Sources")]
    public AudioSource musicSource; // For theme music
    public AudioSource sfxSource;   // For SFX

    [Header("Audio Clips")]
    public AudioClip themeMusic;
    public AudioClip bombDescentExplosion;
    public AudioClip kill;
    public AudioClip newTarget;
    public AudioClip ouch1;
    public AudioClip ouch2;
    public AudioClip mechantDeath;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(themeMusic);
    }

    // Play the theme music
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    // Play an SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // Convenience methods for specific SFX
    public void PlayBombDescentExplosion() => PlaySFX(bombDescentExplosion);
    public void PlayKill() => PlaySFX(kill);
    public void PlayNewTarget() => PlaySFX(newTarget);
    public void PlayOuch1() => PlaySFX(ouch1);
    public void PlayOuch2() => PlaySFX(ouch2);
    public void PlayMechantDeath() => PlaySFX(mechantDeath);

    // --- Pause/Stop music for a duration ---
    public void StopMusicForDuration(float duration)
    {
        StartCoroutine(StopMusicCoroutine(duration));
    }

    private IEnumerator StopMusicCoroutine(float duration)
    {
        if (musicSource.isPlaying)
            musicSource.Pause(); // Pause the music

        yield return new WaitForSeconds(duration); // Wait for the given duration

        musicSource.UnPause(); // Resume music
    }
}
