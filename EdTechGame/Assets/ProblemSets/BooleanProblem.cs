using System;

namespace Assets.ProblemSets
{
    public class BooleanProblem : Problem
    {
        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Boolean Types") +
                    "Booleans are a fancy name but are a very simple variable type." +
                    "Boolean have two values possible: True or False. Notice how the values start with capital letters? This is important!" +
                    "You need to use True or False over true or false in order for the value to work." +
                    "Booleans are used to represent the validity of a fact. " +
                    "For example, if the fact is '10 is greater than 5', then the fact is True. " +
                    "If the fact is 'pigs have wings', then the fact is False.";
            }
        }

        public override string StartCode
        {
            get
            {
                return $"bool = True{Environment.NewLine}print bool";
            }
        }

        public override string Answer
        {
            get
            {
                return "False";
            }
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = NOTHING_PRINTED_HINT;
                return false;
            }

            codeOutput = codeOutput.Trim();

            if (codeOutput == this.Answer)
            {
                hint = "";
                return true;
            }

            if (codeOutput == "True")
            {
                hint = "Your code is still printing the boolean value True. Change to False.";
                return false;
            }

            if (codeOutput == "false" || codeOutput == "true")
            {
                hint = "You need to print a boolean and not a string that just says 'false' or 'true'.";
                return false;
            }

            hint = "Your code should print a single line and that single line should be the false boolean.";
            return false;
        }
    }
}
