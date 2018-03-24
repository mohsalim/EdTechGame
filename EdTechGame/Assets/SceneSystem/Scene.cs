using System;
using System.Collections.Generic;
using Assets.ProblemSets;

namespace Assets.SceneSystem
{
    [Serializable]
    public class Scene
    { 
        /// <summary>
        /// Queue of dialogue sets.
        /// </summary>
        public Queue<Dialogue> Dialogues;

        /// <summary>
        /// Queue of problems.
        /// </summary>
        public Queue<Problem> Problems;

        /// <summary>
        /// Attempt answer. Returns succes boolean. Outs a response (hint or success message).
        /// </summary>
        /// <param name="codeOutput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool AttemptAnswer(string codeOutput, out string response)
        {
            if (Problems.Peek().ValidateAnswer(codeOutput, out response))
            {
                response = Dialogues.Peek().successMessage;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Dequeue next dialogue and problem.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="problem"></param>
        public void Dequeue(out Dialogue dialogue, out Problem problem)
        {
            dialogue = Dialogues.Dequeue();
            problem = Problems.Dequeue();
        }

        /// <summary>
        /// Checks to see if dialogues and problems are all dequeued.
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return Dialogues.Count == 0 && Problems.Count == 0;
        }
    }
}
