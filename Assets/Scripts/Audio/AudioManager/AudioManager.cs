using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        
        audioSource = GetComponent<AudioSource>();

        
        float volumenGuardado = PlayerPrefs.GetFloat("PlayerVolume", 1f);
        audioSource.volume = volumenGuardado;
    }

    
    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("PlayerVolume", value);
    }

    public void SetMusic(AudioClip newMusic)
    {
        if (audioSource.clip == newMusic) return;
        audioSource.clip = newMusic;
        audioSource.Play();
    }
}