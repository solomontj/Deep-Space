using UnityEngine;

public class ProximityDialogueTrigger : MonoBehaviour
{
    public CharacterMonologue characterMonologue; // Assign in inspector

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
            characterMonologue.HideDialogue();  // Optional: Hide dialogue when player leaves
        }
    }
}
