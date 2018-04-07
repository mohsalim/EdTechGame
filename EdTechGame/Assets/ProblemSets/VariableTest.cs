using Assets.Tutorials;
using System;

namespace Assets.ProblemSets
{
    public class VariableTest : Problem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "I think we're at the real meat of the homework now bro!",
                    "Here we got to use all we learned about variables and types so far.",
                    "Read the instructions carefully bro.",
                    $"The instructions say to {ProblemInstructions}"
                },
                successMessage = "That was amazin' bro!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Variables and Types Test") +
                    "Time to test what you've learned so far! Remember that:\n" +
                    " - Strings are words surrounded by single or double quotes such as 'hello', 'Hi friend.', '7', \"I'm the 3rd person to to write this string!\"\n" +
                    " - Integers are whole numbers such as -7, 4, and 2.\n" +
                    " - Floating points are numbers with decimal places such as 0.94719, -12.34, 9.0.\n" +
                    " - Booleans are True or False.\n\n" +
                    $"Please {ProblemInstructions}";
            }
        }

        private string ProblemInstructions
        {
            get
            {
                return $"print the integer {IntegerAnswer}, the floating point {FloatingPointAnswer}, the boolean {BooleanAnswer}, and the string '{StringAnswer}'. " +
                    "It must be printed in that exact order. Each item must be printed on it's own line.";
            }
        }

        public override string Answer
        {
            get
            {
                return $"{IntegerAnswer}{Environment.NewLine}" +
                    $"{FloatingPointAnswer}{Environment.NewLine}" +
                    $"{BooleanAnswer}{Environment.NewLine}" +
                    $"{StringAnswer}";
            }
        }

        /// <summary>
        /// Integer part of the answer.
        /// </summary>
        private string IntegerAnswer
        {
            get
            {
                return "-9";
            }
        }

        /// <summary>
        /// Floating point part of the answer.
        /// </summary>
        private string FloatingPointAnswer
        {
            get
            {
                return "0.682";
            }
        }

        /// <summary>
        /// Boolean part of the answer.
        /// </summary>
        private string BooleanAnswer
        {
            get
            {
                return "True";
            }
        }

        /// <summary>
        /// String part of the answer.
        /// </summary>
        private string StringAnswer
        {
            get
            {
                return "This is my 2nd string!";
            }
        }

        public override string StartCode
        {
            get
            {
                return $"# This is a comment. Comments are usually used to help explain what the code is doing (or will do).{Environment.NewLine}" +
                    $"# You have no starting code this time. Good luck!{Environment.NewLine}";
            }
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"Your code isn't printing anything. {NpcNames.BROSEPH_HINT_PREFIX}{ProblemInstructions}";
                return false;
            }

            codeOutput = codeOutput.Trim();

            if(codeOutput == this.Answer)
            {
                hint = "";
                return true;
            }

            foreach(string part in new string[] { IntegerAnswer, FloatingPointAnswer, BooleanAnswer, StringAnswer })
            {
                if (!codeOutput.Contains(part))
                {
                    hint = $"{NpcNames.BROSEPH_HINT_PREFIX}your code is missing {part}. " +
                        $"Make sure to print it exactly how the instructions say and on it's own line.";
                    return false;
                }
            }

            hint = $"Your code isn't printing anything on the instructions. {NpcNames.BROSEPH_HINT_PREFIX}{ProblemInstructions}";
            return false;
        }
    }
}
