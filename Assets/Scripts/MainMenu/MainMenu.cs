using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject buttonsPanel;

    public TMP_Dropdown qualityDropDown;

    public GameObject mainPlayerSkin;
    public Sprite[] spritesSkins;
    public string[] skinNames = { "Blue", "Purple", "Red", "Yellow" };

    public Slider barraAudio;
    public Slider barraSFX;

    private void Start()
    {
        PlayerPrefs.SetString("MainPlayerSkin", skinNames[0]);

        int savedQuality = PlayerPrefs.GetInt("QualityLevel", -1);

        if (savedQuality == -1)
        {
            savedQuality = QualitySettings.GetQualityLevel();
        }

        qualityDropDown.ClearOptions();
        qualityDropDown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));

        qualityDropDown.value = savedQuality;

        qualityDropDown.onValueChanged.AddListener(OnQualityChange);

        ActualizarBarras();
    }

    public void PlayGame()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }

        Application.Quit();
    }

    public void OpenOptions()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        buttonsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        ActualizarBarras();
    }

    public void CloseOptions()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        buttonsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OnQualityChange(int index)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        QualitySettings.SetQualityLevel(index, true);
        PlayerPrefs.SetInt("QualityLevel", index);
    }

    public void SelectSkin(int index)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        mainPlayerSkin.GetComponent<Image>().sprite = spritesSkins[index];
        PlayerPrefs.SetString("MainPlayerSkin", skinNames[index]);
    }

    private void ActualizarBarras()
    {

        float volumenMusica = PlayerPrefs.GetFloat("PlayerVolume", 1f);
        float volumenSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (barraAudio != null) barraAudio.value = volumenMusica;
        if (barraSFX != null) barraSFX.value = volumenSFX;
    }

    public void CambiarVolumenMusica(float valor)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetVolume(valor);
        }
    }

    public void CambiarVolumenSFX(float valor)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.SetVolume(valor);
        }
    }
}   