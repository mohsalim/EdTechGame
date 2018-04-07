using System;
using System.Collections.Generic;
using Assets.ProblemSets;

namespace Assets.Tutorials
{
    [Serializable]
    public class Tutorial
    { 
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
                // It's ok to recreate dialogue set to get success message. Not much of a cost and doesn't affect reference.
                response = Problems.Peek().GetDialogue().successMessage;
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
            problem = Problems.Dequeue();
            dialogue = problem.GetDialogue();
        }

        /// <summary>
        /// Checks to see if dialogues and problems are all dequeued.
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return Problems.Count == 0;
        }
    }
}
