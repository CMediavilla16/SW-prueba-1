using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource sfxSource;

    // 1. NOMBRES CLAVE: Para que sea fácil llamar a los sonidos por código
    public enum SoundType { swordHit, swordNoHit, buttonClick, gameOver 
}

    // 2. LIBRERÍA: Aquí es donde arrastras los sonidos en el Inspector de Unity
    [Header("Librería de Sonidos")]
    public AudioClip swordHit;
    public AudioClip swordNoHit;
    public AudioClip buttonClick;
    public AudioClip gameOver;

    void Awake()
    {
        // Singleton: Para que solo haya uno y no se destruya
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = GetComponent<AudioSource>();

        // CARGA DE VOLUMEN: Respetamos tu sistema de PlayerPrefs
        float volumenGuardado = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSource.volume = volumenGuardado;
    }

    // --- REPRODUCCIÓN ---

    // Este es el nuevo para usar la librería (Ej: PlayStandardSound(SoundType.Espada))
    public void PlayStandardSound(SoundType tipo)
    {
        switch (tipo)
        {
            case SoundType.swordHit: sfxSource.PlayOneShot(swordHit, 300.0f); break;
            case SoundType.buttonClick: sfxSource.PlayOneShot(buttonClick, 800.0f); break;
            case SoundType.swordNoHit: sfxSource.PlayOneShot(swordNoHit,300.0f); break;
            case SoundType.gameOver: sfxSource.PlayOneShot(gameOver); break;

        }
    }

    // Mantenemos este por si quieres pasarle un clip directamente (como hacíamos antes)
    public void PlaySound(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    // --- CONTROL DE VOLUMEN (EL QUE NO QUEREMOS ROMPER) ---
    public void SetVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}   