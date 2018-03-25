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
                    "The variable \"x\" has a value of 7. Change it to the value to 8 and print the variable \"x\".";
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
