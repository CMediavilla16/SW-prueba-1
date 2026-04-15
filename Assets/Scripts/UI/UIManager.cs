using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject inventory;
    public GameObject pauseMenu;
    public GameObject statsPanel;
    public GameObject statsPanelButton;
    public GameObject pauseMenuButton;
    public GameObject questPanel;
    public GameObject questPanelButton;
    public GameObject diePanel;
    public GameObject storyPanel;
    public GameObject startStoryText;
    public GameObject endStoryText;
    
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject dialoguePanelEnemy;
    public TMP_Text dialogueTextEnemy;

    public TMP_Text moneyCountText;
    public TMP_Text meatCountText;
    public TMP_Text woodCountText;

    public TMP_Text healthText;


    public TMP_Text maxHealthText;
    public TMP_Text levelText;
    public TMP_Text attackDamageText;
    public TMP_Text speedText;
    public Slider xpSlider;

    public TMP_Text requiredMoneyCountText;
    public TMP_Text requiredWoodCountText;
    public TMP_Text requiredMeatCountText;
    public TMP_Text questText;


    
    public static UIManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void OpenOrCloseInventory()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        inventory.SetActive(!inventory.activeSelf);
    }

    public void OpenOrCloseStatsPlayer()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        if (statsPanel.activeSelf == true)
        {
            statsPanel.GetComponent<Animator>().Play("Close");
            statsPanelButton.SetActive(true);
        }
        else
        {
            statsPanel.SetActive(true);
            statsPanel.GetComponent<Animator>().Play("Open");
            statsPanelButton.SetActive(false);
        }

    }

    public void UpdateMoney(int value)
    {
        moneyCountText.text = value.ToString();
    }

    public void UpdateMeat(int value)
    {
        meatCountText.text = value.ToString();
    }

    public void UpdateWood(int value)
    {
        woodCountText.text = value.ToString();
    }


    public void UpdateHealth(int hpValue, int maxHealthValue)
    {
        healthText.text = hpValue.ToString();
        maxHealthText.text = maxHealthValue.ToString();
    }

    public void UpdatePlayerStats(int xpValue, int levelValue, float speedValue, int attackDamageValue)
    {
        xpSlider.value = xpValue;
        levelText.text = levelValue.ToString();
        speedText.text = speedValue.ToString();
        attackDamageText.text = attackDamageValue.ToString();
    }


    public void PauseGame()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        pauseMenu.SetActive(true);
        pauseMenuButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        pauseMenu.SetActive(false);
        pauseMenuButton.SetActive(true);
        Time.timeScale = 1;
    }


    public void GoToMainMenu()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.buttonClick);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

    }


    public void ShowQuestPanel()
    {
        questPanel.SetActive(true);
        questPanelButton.SetActive(false);
    }

    public void HideQuestPanel()
    {
        questPanel.SetActive(false);
        questPanelButton.SetActive(true);
    }


    public void UpdateRequiredResourcesQuestAndName(int moneyAmount, int woodAmount, int meatAmount, string questName)
    {
        requiredMoneyCountText.text = moneyAmount.ToString();
        requiredWoodCountText.text = woodAmount.ToString();
        requiredMeatCountText.text = meatAmount.ToString();

        questText.text = questName.ToString();

    }


    public void DiePanelAnimation()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayStandardSound(SFXManager.SoundType.gameOver);
        }
        diePanel.SetActive(true);
    }


    public void ShowDialogue(string message)
    {
        dialogueText.text = message; // Cambia el texto por el que le pasemos
        dialoguePanel.SetActive(true); // Enciende el panel
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false); // Apaga el panel
    }



    public void ShowDialogueEnemy(string message2)
    {
        dialogueTextEnemy.text = message2; // Cambia el texto por el que le pasemos
        dialoguePanelEnemy.SetActive(true); // Enciende el panel
    }

    public void HideDialogueEnemy()
    {
        dialoguePanelEnemy.SetActive(false); // Apaga el panel
    }



    public void StartStory()
    {
        startStoryText.SetActive(true);
        endStoryText.SetActive(false);
        storyPanel.SetActive(true);
        Invoke("CloseStory", 10);
    }

    public void EndStory()
    {
        startStoryText.SetActive(false);
        endStoryText.SetActive(true);
        storyPanel.SetActive(true);
        Invoke("CloseStoryAndGoToMainMenu", 10);
    }


    public void CloseStory()
    {

        storyPanel.SetActive(false);
    }

    public void CloseStoryAndGoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




}
