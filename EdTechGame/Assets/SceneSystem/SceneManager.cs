using Assets.ProblemSets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SceneSystem
{
    public class SceneManager : MonoBehaviour
    {
        public InputField CodeInputField;

        public Text DialogueText;

        public Dialogue currentDialogue = null;

        public Problem currentProblem = null;

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
                                        "I think we have to get 'hello word' followed by 'goodbye world', but I'm not sure how to do that!"
                                    },
                                    successMessage =  "Nice bro! I think that was right!",
                                },
                                new Dialogue
                                {
                                    name = NpcNames.BROSEPH,
                                    sentences = new string[]
                                    {
                                        "The assignment says that we to change the variable 'number' to 8.",
                                        "But bro, it also says we got to do it befre printing the variable."
                                    },
                                    successMessage = "That seems right. You're so smart bro!"
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
        public void StartNextScene()
        {
            Scenes[sceneCounter].Dequeue(out currentDialogue, out currentProblem);
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue);
            CodeInputField.text = currentProblem.StartCode;
            sceneCounter++;
        }
    }
}
