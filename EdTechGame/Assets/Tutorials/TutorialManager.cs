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
        /// Current dialogue set.
        /// </summary>
        public Dialogue currentDialogue = null;

        /// <summary>
        /// Current problem player needs to complete.
        /// </summary>
        public Problem currentProblem = null;

        /// <summary>
        /// Scene counter to keep track of player progress.
        /// </summary>
        private int sceneCounter = 0;

        /// <summary>
        /// Array of scenes for the entire game.
        /// </summary>
        private Scene[] Scenes
        {
            get
            {
                return new Scene[]
                {
                    new Scene()
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
        /// Start next dialogue and prblem.
        /// </summary>
        public void StartNextProblem()
        {
            Scenes[sceneCounter].Dequeue(out currentDialogue, out currentProblem);
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue);
            CodeInputField.text = currentProblem.StartCode;
            InstructionText.text = currentProblem.Instructions;
            sceneCounter++;
        }
    }
}
