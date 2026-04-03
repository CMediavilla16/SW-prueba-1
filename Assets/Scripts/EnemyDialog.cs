using UnityEngine;

public class EnemyDialog : MonoBehaviour
{
    [TextArea] // Esto hace que la cajita para escribir en Unity sea más grande y cómoda
    public string messageToSay = "E";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador entra en el área, mostramos el mensaje que hayamos escrito
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowDialogueEnemy(messageToSay);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el jugador sale del área, escondemos el panel
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideDialogueEnemy();
        }
    }
}
