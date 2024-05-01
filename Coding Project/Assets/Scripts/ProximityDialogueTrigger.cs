using UnityEngine;

public class ProximityDialogueTrigger : MonoBehaviour
{
    public CharacterMonologue characterMonologue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            characterMonologue.TriggerRobotDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            characterMonologue.HideDialogue();
        }
    }
}