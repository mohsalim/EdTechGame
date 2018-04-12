using Assets.Beautifier;
using Assets.ProblemSets;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Tutorials
{
    public class TutorialManager : MonoBehaviour
    {
        /// <summary>
        /// One-way flag to used to control thank you message after completing game.
        /// </summary>
        private const string GAME_COMPLETE_FLAG = "GameComplete";

        /// <summary>
        /// Animator object.
        /// </summary>
        public Animator AnimatorInterface;

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
        /// Button for running code.
        /// </summary>
        public Button RunCodeButton;

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
        private static Tutorial[] Tutorials = new Tutorial[]
        {
            TutorialSet.GetTypesAndArithmeticTutorial(),
            TutorialSet.GetStringsAndListsTutorial()
        };

        /// <summary>
        /// Start next dialogue and problem.
        /// </summary>
        public void StartNextProblem()
        {
            Debug.Log("Starting next problem. tutorialCounter = " + tutorialCounter);

            // Hide the next problem button.
            NextProblemButton.gameObject.SetActive(false);

            // Display run code button.
            RunCodeButton.gameObject.SetActive(true);


            // If the counter exceeds tutorials, then we need to end the game properly instead of letting it crash.
            if (Tutorials.Length <= tutorialCounter)
            {
                Debug.Log("Ending game.");
                AnimatorInterface.SetBool(GAME_COMPLETE_FLAG, true);
                return;
            }

            // Get the next dialogue/problem set from the current tutorial.
            Tutorials[tutorialCounter].Dequeue(out currentDialogue, out currentProblem);

            // Start the dialogue.
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue);

            // Display starting code.
            // CodeInputField.text = CodeBeautifier.Beautify(currentProblem.StartCode);
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
