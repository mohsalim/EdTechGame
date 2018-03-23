using Assets.ProblemSets;
using UnityEngine;

namespace Assets.SceneSystem
{
    public class SceneManager : MonoBehaviour
    {
        public void StartScene(Scene scene)
        {
            Dialogue dialogue;
            Problem problem;
            scene.Dequeue(out dialogue, out problem);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            // TODO set up problem!
            // TODO Add onclick event/trigger.
            // TODO Add first sccene trigger.
        }
    }
}
