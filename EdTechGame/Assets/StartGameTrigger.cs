using UnityEngine;
using UnityEngine.UI;

public class StartGameTrigger : MonoBehaviour
{
    /// <summary>
    /// Dialogue for this trigger behavior.
    /// </summary>
    public Dialogue dialogue;

    /// <summary>
    /// Start button.
    /// </summary>
    public Button button;

    /// <summary>
    /// Flag to keep track if button was click in order to start fade.
    /// </summary>
    public bool startFade = false;

    /// <summary>
    /// Update to check if we need to fade.
    /// </summary>
    void Update()
    {
        // If fade has started.
        if (startFade)
        {
            // Compute the alpha value based on the button's image.
            float newAlpha = button.image.color.a - Mathf.Clamp01(Time.deltaTime / 0.5f);
            Debug.Log("New alpha value: " + newAlpha);

            // Decrease alpha value.
            // TODO This doesn't fade children components.
            button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, newAlpha);

            // If the button is faded out.
            if(newAlpha <= 0.0f)
            {
                // Stop the fade.
                startFade = false;

                // Destroy the button.
                // TODO This doesn't destroy children components.
                Destroy(button);

                // Start first dialogue.
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }

    /// <summary>
    /// Triggered on click to start fade.
    /// </summary>
    public void TriggerFade()
    {
        Debug.Log("Triggering fade.");
        startFade = true;
    }
}