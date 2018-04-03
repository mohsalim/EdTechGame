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
        /// Primary audio source object.
        /// </summary>
        public AudioSource AudioSource;

        /// <summary>
        /// Sliding audio sound when opening and closing instruction box.
        /// </summary>
        private AudioClip OpenCloseBoxSound;

        void Start()
        {
            OpenCloseBoxSound = Resources.Load("UI Sfx/Wav/Slide_Sharp_02") as AudioClip;
        }

        /// <summary>
        /// Open instruction box.
        /// </summary>
        public void OpenInstructions()
        {
            OpenCloseInstructions(true);
        }

        /// <summary>
        /// Close instruction box.
        /// </summary>
        public void CloseInstructions()
        {
            OpenCloseInstructions(false);
        }

        /// <summary>
        /// Helper for opening/closing instructions box.
        /// </summary>
        /// <param name="isOpen"></param>
        private void OpenCloseInstructions(bool isOpen)
        {
            // Log based on open or close.
            string action = isOpen ? "Opening" : "Closing";
            Debug.Log($"{action} instructions box.");

            // Animate open/close animation.
            AnimatorInterface.SetBool(INSTRUCTION_BOX_FLAG, isOpen);

            // Playing open/close audio.
            AudioSource.PlayOneShot(OpenCloseBoxSound);
        }
    }
}
