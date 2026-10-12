using UnityEngine;

public class AudioScripting : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource jingleSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource footstepSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip museumMusic;
    [SerializeField] private AudioClip minigameMusic;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip termiteCaughtSound;
    [SerializeField] private AudioClip steppingSound;
    [SerializeField] private AudioClip spotlightSound;
    [SerializeField] private AudioClip questioningSound;
    [SerializeField] private AudioClip paintBrushSound;
    [SerializeField] private AudioClip greetingSound;
    [SerializeField] private AudioClip generalClickSound;
    [SerializeField] private AudioClip crowdGaspSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip chompSound;




    [Header("Startup")]
    [SerializeField] private bool startWithMuseumMusic = true;

    private void Start()
    {
        if (startWithMuseumMusic)
        PlayMuseumMusic();
    }

    public void PlayMuseumMusic()
    {
        PlayMusic(museumMusic);
    }

    [ContextMenu("Test Minigame")]
    public void PlayMinigameMusic()
    {
        PlayMusic(minigameMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;
        
        jingleSource.Stop();

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;
        
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    [ContextMenu("Test Win")]
    public void PlayWin()
    {
        PlayJingle(winSound);
    }

    [ContextMenu("Test Lose")]
    public void PlayLose()
    {
        PlayJingle(loseSound);
    }

    private void PlayJingle(AudioClip clip)
    {
        if (clip == null)
            return;
        
        StopFootsteps();
        musicSource.Stop();
        jingleSource.Stop();
        jingleSource.clip = clip;
        jingleSource.loop = false;
        jingleSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void StopAllAudio()
    {
        musicSource.Stop();
        jingleSource.Stop();
        sfxSource.Stop();
        footstepSource.Stop();
    }




    public void PlayTermiteCaught() { PlaySFX(termiteCaughtSound);}
    public void PlaySpotlight() { PlaySFX(spotlightSound);}
    public void PlayQuestioning() { PlaySFX(questioningSound);}
    public void PlayPaintBrush() { PlaySFX(paintBrushSound);}
    public void PlayGreeting() { PlaySFX(greetingSound);}
    public void PlayGeneralClick() { PlaySFX(generalClickSound);}
    public void PlayCrowdGasp() { PlaySFX(crowdGaspSound);}
    public void PlayClick() { PlaySFX(clickSound);}

    [ContextMenu("Test Chomp")]
    public void PlayChomp() { PlaySFX(chompSound);}

public void StartFootsteps()
    {
        if (steppingSound == null || footstepSource.isPlaying)
        return;

        footstepSource.clip = steppingSound;
        footstepSource.loop = true;
        footstepSource.Play();
    }

    public void StopFootsteps()
    {
        footstepSource.Stop();
    }




}
