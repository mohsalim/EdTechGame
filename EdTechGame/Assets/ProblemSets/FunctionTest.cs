using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class FunctionTest : FunctionProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.HACKER,
                sentences = new string[]
                {
                    $"I think you're ready. I want you to write this next set of code at an endpoint I'm about to send your way.",

                    $"Don't mess this up! Your grades depend on it.",

                    $"After this the police will finally be off my tail...ahem...I mean you'll finally get your revenge on {NpcNames.BROSEPH}",

                    this.TaskInstructions

                },
                successMessage = "Perfect! I would take your time getting home if I were you...err...because the weather is amazing. (You hang up and head home. As you get home, cops surround and cuff you.)",
                sprite = NpcNames.HACKER_SPRITE_EVIL
            };
        }

        public override string TaskInstructions
        {
            get
            {
                return $"For this task, create a function called {FunctionName} that will be given a single integer parameter. " +
                    $"Create a variable with the value 0." +
                    $"You will iterate from 0 and up to the parameter (exclusively!). " +
                    $"As you iterate, you set the variable to the item in the list plus variable itself (ranged sum)" +
                    $"After the loop, print your variable. " +
                    $"Call your function with arguments: {ParamA}, {ParamB}, and {ParamC}.";
            }
        }

        private string FunctionName
        {
            get
            {
                return "sum_range";
            }
        }

        private string ParamA
        {
            get
            {
                return "5";
            }
        }

        private string ParamB
        {
            get
            {
                return "90";
            }
        }

        private string ParamC
        {
            get
            {
                return "1000";
            }
        }

        public override string StartCode
        {
            get
            {
                return $"# Create a function that prints the ranged sum for a given parameter. The range should be 0 to the parameter (exclusively!).{Environment.NewLine}" +
                    $"# Call this function for the parameters {ParamA}, {ParamB}, and {ParamC}. Good luck!{Environment.NewLine}";
            }
        }

        public override string Answer
        {
            get
            {
                return $"10{Environment.NewLine}4005{Environment.NewLine}499500";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            // Empty/null case.
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"Your code isn't printing anything. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Trim and split code into code lines.
            code = code.Trim();
            string[] codeLines = StringUtils.SplitByLines(code);

            // Do we add?
            if (!StringUtils.HasPlusOperator(codeLines))
            {
                hint = $"Your code doesn't use the addition (+) operator. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we use correct def format?
            if (!StringUtils.HasFunctionDefinition(codeLines))
            {
                hint = $"Your code doesn't format and use the 'def' correctly. You need the keywords 'def', parentheses and the colon (:). For example 'def func_name():'. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Check function names.
            if (!StringUtils.HasCodeString(codeLines, FunctionName))
            {
                hint = $"Your code is missing the function {FunctionName}. Make sure the spelling is exactly how I said it. {NpcNames.HACKER_HINT_PREFIX}{TaskInstructions}";
                return false;
            }

            // Check to see if the multiples are used.
            bool hasParams = StringUtils.HasCodeString(codeLines, ParamA)
                && StringUtils.HasCodeString(codeLines, ParamB)
                && StringUtils.HasCodeString(codeLines, ParamC);
            if (!hasParams)
            {
                hint = $"Your code is missing the parameters for the function calls: {ParamA}, {ParamB}, and {ParamC}. {NpcNames.HACKER_HINT_PREFIX}{TaskInstructions}";
                return false;
            }

            // Do we use the proper for loop.
            if (!StringUtils.HasForLoop(codeLines))
            {
                hint = $"Your code doesn't format and use the for loop correctly. You need the keywords 'for' and 'in' as well as the colon (:). For example 'for x in xs:'. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we use the range function?
            if (!StringUtils.HasRangeFunction(codeLines))
            {
                hint = $"Your code doesn't seem to generate the list. You need to use the range() function to generate the list. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Is the answer correct?
            codeOutput = codeOutput.Trim();
            if (codeOutput == Answer)
            {
                hint = "";
                return true;
            }

            // Make sure it's 3 lines of code.
            string[] codeOutputLines = StringUtils.SplitByLines(codeOutput);
            string[] answerLines = StringUtils.SplitByLines(Answer);
            if (codeOutputLines.Length != answerLines.Length)
            {
                hint = $"Your output prints too {StringUtils.GetFewOrMany(answerLines.Length, codeOutputLines.Length)} things. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Find out if a specific output is wrong by line.
            for (int i = 0; i < answerLines.Length; i++)
            {
                if (codeOutputLines[i] != answerLines[i])
                {
                    hint = $"Your output prints the wrong {StringUtils.GetNthText(i + 1)} item. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                    return false;
                }
            }

            // Some other unknown error.
            hint = $"Your output seems wrong. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
            return false;
        }
    }
}
