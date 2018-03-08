using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    /// <summary>
    /// Dialogue for this trigger behavior.
    /// </summary>
    public Dialogue dialogue;

    /// <summary>
    /// Find the dialogue manager and start the dialogue.
    /// </summary>
    public void TriggerDialogue()
    {
        Debug.Log("Triggered dialogue.");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
