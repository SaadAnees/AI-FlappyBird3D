using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip[] backgroundMusic;

    private AudioSource audioSource;

    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
        DontDestroyOnLoad(gameObject);

    }

    public void PlayJumpSound()
    {
        PlaySound(jumpClip);
    }
    public void PlayButtonSound()
    {
        PlaySound(buttonClip);
    }


    public void PlayDieSound()
    {
        PlaySound(dieClip);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameOverClip);
    }

    public void PlayCollectSound()
    {
        PlaySound(collectClip);
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length - 1)];
        audioSource.loop = true;
        audioSource.Play();
    }
}
