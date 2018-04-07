using System;

namespace Assets.ProblemSets
{
    /// <summary>
    /// Programming problem.
    /// </summary>
    [Serializable]
    public abstract class Problem
    {
        /// <summary>
        /// Default nothing is printing text.
        /// </summary>
        protected const string NOTHING_PRINTED_HINT = "Your code isn't printing anything.";

        /// <summary>
        /// Title format for instructions.
        /// </summary>
        protected const string TITLE_FORMAT = "<size=32><b>{0}</b></size>\n\n";

        /// <summary>
        /// Starting code.
        /// </summary>
        public abstract string StartCode { get; }

        /// <summary>
        /// Correct answer.
        /// </summary>
        public abstract string Answer { get; }

        public abstract string Instructions { get; }

        /// <summary>
        /// Returns true if answer is correct.
        /// Returns false if answer is incorrect and out's a hint.
        /// </summary>
        /// <param name="codeOutput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public abstract bool ValidateAnswer(string codeOutput, out string hint);

        /// <summary>
        /// Gets dialogue set for this problem.
        /// </summary>
        /// <returns></returns>
        public abstract Dialogue GetDialogue();
    }
}