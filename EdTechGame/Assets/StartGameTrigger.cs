using Assets.Tutorials;
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
    /// Text component on button. Needed so we can also fade it along with button.
    /// </summary>
    public Text buttonText;

    /// <summary>
    /// Flag to keep track if button was click in order to start fade.
    /// </summary>
    public bool startFade = false;

    /// <summary>
    /// Has the button been completely faded?
    /// </summary>
    private bool buttonFaded = false;

    /// <summary>
    /// Has the button text been completely faded?
    /// </summary>
    private bool buttonTextFaded = false;

    /// <summary>
    /// Update to check if we need to fade.
    /// </summary>
    void Update()
    {
        // If fade has started.
        if (startFade)
        {
            // If the butotn hasn't faded.
            if (!buttonFaded)
            {
                // Compute the alpha value based on the button's image.
                float newAlphaButton = button.image.color.a - Mathf.Clamp01(Time.deltaTime / 0.5f);
                Debug.Log("New button alpha value: " + newAlphaButton);

                // Decrease alpha value.
                button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, newAlphaButton);

                // Update flag tracking button faded or not.
                buttonFaded = newAlphaButton <= 0.0f;
            }

            // If the button text hasn't faded.
            if (!buttonTextFaded)
            {
                // Compute the alpha value based on the button's image.
                float newAlphaButtonText = buttonText.color.a - Mathf.Clamp01(Time.deltaTime / 0.5f);
                Debug.Log("New button text alpha value: " + newAlphaButtonText);

                // Decrease alpha value.
                buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, newAlphaButtonText);

                // Update flag tracking button text faded or not.
                buttonTextFaded = newAlphaButtonText <= 0.0f;
            }

            // If the button is faded out.
            if (buttonFaded && buttonTextFaded)
            {
                // Stop the fade.
                startFade = false;

                // Destroy the button and button text.
                Destroy(button);
                Destroy(buttonText);

                // Start first scene.
                FindObjectOfType<TutorialManager>().StartNextProblem();
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