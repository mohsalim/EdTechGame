using System;

namespace Assets.ProblemSets
{
    public class NumberVariableProblem : Problem
    {
        public override string StartCode
        {
            get
            {
                return $"number = 7{Environment.NewLine}print number";
            }
        }

        public override string Answer
        {
            get
            {
                return "8";
            }
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

            hint = $"Trying changing the number variable from 7 to {this.Answer}.";
            return false;
        }
    }
}
