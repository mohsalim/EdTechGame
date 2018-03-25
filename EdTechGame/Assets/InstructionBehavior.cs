using UnityEngine;

namespace Assets
{
    public class InstructionBehavior : MonoBehaviour
    {
        /// <summary>
        /// Flag for opening/closing instruction box.
        /// </summary>
        private const string INSTRUCTION_BOX_FLAG = "IsInstructionsOpen";

        /// <summary>
        /// Animator object.
        /// </summary>
        public Animator AnimatorInterface;

        /// <summary>
        /// Open instruction box.
        /// </summary>
        public void OpenInstructions()
        {
            Debug.Log("Opening instructions box.");
            AnimatorInterface.SetBool(INSTRUCTION_BOX_FLAG, true);
        }

        /// <summary>
        /// Close instruction box.
        /// </summary>
        public void CloseInstructions()
        {
            Debug.Log("Closing instructions box.");
            AnimatorInterface.SetBool(INSTRUCTION_BOX_FLAG, false);
        }
    }
}
