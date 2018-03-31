using Assets.Sprites;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages and starts given dialogues.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// Name of flag for tracking if the dialogue box is open or closed.
    /// </summary>
    private const string IS_OPEN_FLAG = "IsGameCanvasOpen";

    /// <summary>
    /// Text for NPC's name.
    /// </summary>
    public Text NameText;

    /// <summary>
    /// Image for holding NPC's sprite.
    /// </summary>
    public Image NpcImage;

    /// <summary>
    /// Text for current sentence.
    /// </summary>
    public Text DialogueText;

    /// <summary>
    /// Button for contining to next sentence.
    /// </summary>
    public Button ContinueButton;

    /// <summary>
    /// Button for starting next problem.
    /// </summary>
    public Button NextProblemButton;

    /// <summary>
    /// Button for running code.
    /// </summary>
    public Button RunCodeButton;

    /// <summary>
    /// Animator for opening/closing dialogue box.
    /// </summary>
    public Animator AnimatorInterface;

    /// <summary>
    /// Queue of setences.
    /// </summary>
    private Queue<string> Sentences;

    /// <summary>
    /// Use this for initialization 
    /// </summary>
    void Start()
    {
        Sentences = new Queue<string>();
    }

    /// <summary>
    /// Starts dialogue queue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue for NPC: " + dialogue.name);

        // Update sprite.
        NpcImage.sprite = SpriteCache.GetSprite(dialogue.sprite);

        // Clear out the current queue.
        Sentences.Clear();

        // Enqueue the sentences from the given dialogue.
        foreach (string sentence in dialogue.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        StartSentenceHelper(dialogue);
    }

    /// <summary>
    /// Clear dialogue and display success message.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartSuccessMessage(Dialogue dialogue)
    {
        Debug.Log("Starting success message for NPC: " + dialogue.name);

        // Hide run code button. It should only be displayed after next problem is started.
        // This is done in the TutorialManager.cs.
        RunCodeButton.gameObject.SetActive(false);

        // Clear out the current queue.
        Sentences.Clear();

        // Enqueue the sentences from the given dialogue.
        Sentences.Enqueue(dialogue.successMessage);

        StartSentenceHelper(dialogue);

        // Show the next problem button.
        NextProblemButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Mutual logic for displaying dialogue or success message (which both set the name).
    /// </summary>
    /// <param name="dialogue"></param>
    private void StartSentenceHelper(Dialogue dialogue)
    {
        // Update the NPC's name.
        NameText.text = dialogue.name;
        StartSentenceAnimation();
    }

    /// <summary>
    /// Mutual logic for displaying animating.
    /// </summary>
    private void StartSentenceAnimation()
    {
        // Show continue button.
        Debug.Log("Showing continue button");
        ContinueButton.gameObject.SetActive(true);

        // Open the the box. TODO Should we do this after we've cleared the sentences and updated the names?
        AnimatorInterface.SetBool(IS_OPEN_FLAG, true);

        // Start displaying the new sentences.
        DisplayNextSentence();
    }

    /// <summary>
    /// Start hint. Does not change NPC name.
    /// </summary>
    /// <param name="hint"></param>
    public void StartHint(string hint)
    {
        Debug.Log("Displaying hint for current NPC: " + NameText.text);

        // Clear out the current queue.
        Sentences.Clear();

        // Enqueue the hint.
        Sentences.Enqueue(hint);

        StartSentenceAnimation();
    }

    /// <summary>
    /// Display next sentence in queue.
    /// </summary>
    public void DisplayNextSentence()
    {
        // Get the next sentence in the queue.
        string sentence = Sentences.Dequeue();

        // Stop other coroutines (say if the user is clicking the "Continue" button too fast).
        StopAllCoroutines();

        // Start a new coroutine to type out the sentence.
        StartCoroutine(TypeSentence(sentence));

        // If there are no more sentences.
        if (Sentences.Count == 0)
        {
            // Hide continue button.
            Debug.Log("Hiding continue button");
            ContinueButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Iterator to type out each sentence character at a time.
    /// Gives the feel that the NPC is actually speaking to you via text.
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        Debug.Log("Typing sentence: " + sentence);

        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
}
