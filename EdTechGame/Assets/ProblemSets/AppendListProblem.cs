using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class AppendListProblem : ListProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    $"{WhisperPrefix}Oh man this task looks so freaking hard...",
                    "I hope I can figure it out. We have to append to a list. Appending is adding an item to the list at the end of the list I think.",
                    "I think I have to use the append() method. This is different from len() function. " +
                    "The len() function wraps around the variable whereas the append() method comes after variable connected by a period. " +
                    "For example, if the list variable was 'x' then I would do len(x) for length and x.appen(1) to add 1 the list named 'x'.",
                    "len() is a function because it attaches to no variable and append() is a method because it attaches to a variable (or object).",
                    this.TaskInstruction

                },
                successMessage = $"{WhisperPrefix}I don't know any of this... (Gets up and turns to you) " +
                "Hey pal, it looks like you just printed your assignment. I just printed mine too! " +
                "Don't worry about getting up. I'll turn your assignment in for you. I owe you remember!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        private string WhisperPrefix
        {
            get
            {
                return "(Whispering loudly) ";
            }
        }

        protected override string TaskInstruction
        {
            get
            {
                return "For this task, create an empty list and print it. Then append 5 items: 1, 2, 3, 4, 5. " +
                    "After each append, print the list. You should be able to see how the list grows per append function call." +
                    "Finally print the length of the list.";
            }
        }

        public override string StartCode
        {
            get
            {
                return AppendStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                return $"[]{Environment.NewLine}" +
                    $"[1]{Environment.NewLine}" +
                    $"[1, 2]{Environment.NewLine}" +
                    $"[1, 2, 3]{Environment.NewLine}" +
                    $"[1, 2, 3, 4]{Environment.NewLine}" +
                    $"[1, 2, 3, 4, 5]{Environment.NewLine}" +
                    $"5";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            // Empty/null case.
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"{WhisperPrefix}Your code isn't printing anything. {this.TaskInstruction}";
                return false;
            }

            // Trim and split code into code lines.
            code = code.Trim();
            string[] codeLines = StringUtils.SplitByLines(code);

            // Do we use len() function?
            bool hasLengthFunction = StringUtils.HasCodeString(codeLines, "len(");
            if (!hasLengthFunction)
            {
                hint = $"{WhisperPrefix}Your code doesn't use the len() function. {this.TaskInstruction}";
                return false;
            }

            // Do we use the append() method?
            bool hasAppend = StringUtils.HasCodeString(codeLines, ".append(");
            if (!hasAppend)
            {
                hint = $"{WhisperPrefix}My code doesn't use the .append() method. {this.TaskInstruction}";
                return false;
            }

            // Is the answer correct?
            codeOutput = codeOutput.Trim();
            if (codeOutput == Answer)
            {
                hint = "";
                return true;
            }

            // Make sure it's 7 lines of code.
            string[] codeOutputLines = StringUtils.SplitByLines(codeOutput);
            if (codeOutputLines.Length > 7 || codeOutputLines.Length < 7)
            {
                string manyOrFew = codeOutputLines.Length > 7 ? "many" : "few";
                hint = $"{WhisperPrefix}My code prints too {manyOrFew} things. {this.TaskInstruction}";
                return false;
            }

            // TODO Check each item before length and let them know ith append is wrong.
            // TODO Check length and let them know it's wrong.

            // Some unknown use case occurred.
            hint = $"{WhisperPrefix}Your output seems incorrect. {this.TaskInstruction}";
            return false;
        }
    }
}
