using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public Quest[] quests;
    private int currentQuestIndex = 0;

    private bool questActive = false;

    private PlayerResourceCollector player;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerResourceCollector>();
        UIManager.Instance.StartStory();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowQuestPanel();

            if (!questActive)
            {
                questActive = true;
                ShowQuest(quests[currentQuestIndex]);
            }
            else
            {
                Quest currentQuest = quests[currentQuestIndex];

                if (currentQuest.IsCompleted(player.GetWood(), player.GetMoney(), player.GetMeat()))
                {
                    AdvanceQuest();
                }
                else
                {
                    ShowQuest(currentQuest);
                }
            }

        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideQuestPanel();
        }
    }

    public void ShowQuest(Quest quest)
    {
        UIManager.Instance.UpdateRequiredResourcesQuestAndName(quest.moneyRequired,  quest.woodRequired, quest.meatRequired, quest.questname);
        
    }

    public void AdvanceQuest()
    {
        Debug.Log("Quest completada, a por la siguiente");

        player.SetMoney(player.GetMoney() - quests[currentQuestIndex].moneyRequired);
        player.SetWood(player.GetWood() - quests[currentQuestIndex].woodRequired);
        player.SetMeat(player.GetMeat() - quests[currentQuestIndex].meatRequired);

        player.UpdateAllResources();

        if (currentQuestIndex < quests.Length - 1)
        {
            currentQuestIndex++;
            ShowQuest(quests[currentQuestIndex]);
        }
        else
        {
            Debug.Log("Todas las misiones completadas");
            UIManager.Instance.EndStory();
        }

    }


}
