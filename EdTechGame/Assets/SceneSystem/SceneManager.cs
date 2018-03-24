using Assets.ProblemSets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SceneSystem
{
    public class SceneManager : MonoBehaviour
    {
        public Text InputText;

        public Text DialogueText;

        private Dialogue currentDialogue = null;

        private Problem currentProblem = null;

        private int sceneCounter = 0;


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
                                    }
                                },
                                new Dialogue
                                {
                                    name = NpcNames.BROSEPH,
                                    sentences = new string[]
                                    {
                                        "Nice bro! I think that was right!",
                                        "The assignment says that we to change the variable 'number' to 8.",
                                        "But bro, it also says we got to do it befre printing the variable."
                                    }
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

        public void StartNextScene()
        {
            Scenes[sceneCounter].Dequeue(out currentDialogue, out currentProblem);
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue);
            InputText.text = currentProblem.StartCode;
            sceneCounter++;

            // TODO set up problem!
            // TODO Add onclick event/trigger.
            // TODO Add first sccene trigger.
        }
    }
}
