using UnityEngine;

public class MusicChangerGame : MonoBehaviour
{
    public AudioClip newMusic;

    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusic(newMusic);
        }
    }
}