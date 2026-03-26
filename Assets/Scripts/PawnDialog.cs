using UnityEngine;

public class PawnDialogue : MonoBehaviour
{
    [TextArea] // Esto hace que la cajita para escribir en Unity sea más grande y cómoda
    public string messageToSay = "Mas a la izquierda llegarás a la cueva maldita";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador entra en el área, mostramos el mensaje que hayamos escrito
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowDialogue(messageToSay);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el jugador sale del área, escondemos el panel
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideDialogue();
        }
    }
}