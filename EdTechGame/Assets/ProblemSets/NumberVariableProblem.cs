namespace Assets.ProblemSets
{
    public abstract class NumberVariableProblem : Problem
    {
        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Variables and Types") +
                    "Python is completely object oriented. Think of this as \"objects\" are nouns and \"methods\" are verbs.\n\n" +
                    "You do not need to declare variables before using them or declare their type. " +
                    "For example, is the variable a number or a word? It will never have a fixed type. " +
                    "It changes as the value changes. " +
                    "Every variable in Python is an object. " +
                    "This tutorial will go over a few basic types of variables." +
                    "Python supports two types of numbers: \"integers\" (whole numbers like 1, 0, -2) and \"floating point numbers\" (decimal numbers like 0.2, 1.9391, -5.129). " +
                    "(It also supports complex numbers which will not be explained in this tutorial).\n\n" +
                    this.SpecificInstruction;
            }
        }

        /// <summary>
        /// Specific instruction for number variable problem. Will be addeed at the end of the instructions.
        /// </summary>
        protected abstract string SpecificInstruction
        {
            get;
        }

        /// <summary>
        /// Specific hint for number variable problem. Will used in validation of answer.
        /// </summary>
        protected abstract string SpecificHint
        {
            get;
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = NOTHING_PRINTED_HINT;
                return false;
            }

            codeOutput = codeOutput.ToLowerInvariant().Trim();

            if (codeOutput == this.Answer)
            {
                hint = null;
                return true;
            }

            hint = this.SpecificHint;
            return false;
        }
    }
}
