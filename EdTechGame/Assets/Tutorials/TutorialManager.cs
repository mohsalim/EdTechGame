using Assets.ProblemSets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Tutorials
{
    public class TutorialManager : MonoBehaviour
    {
        /// <summary>
        /// Input field for code.
        /// </summary>
        public InputField CodeInputField;

        /// <summary>
        /// Output field for NPC dialogue and hints.
        /// </summary>
        public Text DialogueText;

        /// <summary>
        /// Text for additional instruction.
        /// </summary>
        public Text InstructionText;

        /// <summary>
        /// Button for starting next problem.
        /// </summary>
        public Button NextProblemButton;

        /// <summary>
        /// Current dialogue set.
        /// </summary>
        public Dialogue currentDialogue = null;

        /// <summary>
        /// Current problem player needs to complete.
        /// </summary>
        public Problem currentProblem = null;

        /// <summary>
        /// Tutorial counter to keep track of player progress.
        /// </summary>
        private int tutorialCounter = 0;

        /// <summary>
        /// Array of tutorials for the entire game.
        /// </summary>
        private Tutorial[] Tutorials
        {
            get
            {
                return new Tutorial[]
                {
                    new Tutorial()
                    {
                        Dialogues = new Queue<Dialogue>
                        (
                            new Dialogue[]
                            {
                                new Dialogue
                                {
                                    name = NpcNames.BROSEPH,
                                    sentences = new string[]
                                    {
                                        "Bro, did you finish the programming homework?",
                                        "I can't figure it out! Can you please help me solve it?",
                                        "Come on bro! I'll pay you back big time time.",
                                        "Brooooooooooooooooooooooooooooooooooooooooooooo, please?!",
                                        "I think we have to print 'hello word' followed by 'goodbye world', but I'm not sure how to do that!"
                                    },
                                    successMessage =  "Nice bro! I think that was right!",
                                    sprite = NpcNames.BROSEPH_SPRITE_HAPPY
                                },
                                new Dialogue
                                {
                                    name = NpcNames.BROSEPH,
                                    sentences = new string[]
                                    {
                                        "The assignment says that we to change the variable 'number' to 8.",
                                        "But bro, it also says we got to do it befre printing the variable."
                                    },
                                    successMessage = "That seems right. You're so smart bro!",
                                    sprite = NpcNames.BROSEPH_SPRITE_HAPPY
                                }
                            }
                        ),
                        Problems = new Queue<Problem>
                        (
                            new Problem[]
                            {
                                new HelloWorldProblem(),
                                new NumberVariableProblem()
                            }
                        )
                    }
                };
            }
        }

        /// <summary>
        /// Start next dialogue and problem.
        /// </summary>
        public void StartNextProblem()
        {
            Debug.Log("Starting next problem. tutorialCounter = " + tutorialCounter);

            // Hide the next problem button.
            NextProblemButton.gameObject.SetActive(false);

            // Get the next dialogue/problem set from the current tutorial.
            Tutorials[tutorialCounter].Dequeue(out currentDialogue, out currentProblem);

            // Start the dialogue.
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue);

            // Display starting code.
            CodeInputField.text = currentProblem.StartCode;

            // Update instructions.
            InstructionText.text = currentProblem.Instructions;

            // If there are no more problems in the current tutorial, then increment the tutorial counter.
            if (Tutorials[tutorialCounter].IsComplete())
            {
                Debug.Log("Tutorial " + tutorialCounter + " now complete.");
                tutorialCounter++;
            }
        }
    }
}
