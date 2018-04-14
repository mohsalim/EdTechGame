using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class FunctionBasicsProblem : FunctionProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.HACKER,
                sentences = new string[]
                {
                    $"(As you're walking home, you get a call from an unknown number) Do you want revenge on {NpcNames.BROSEPH}?",

                    "You know? The guy who screwed you over and got you an F? " +
                    "I have accesss to all his grades and I can give him the F he deserves.",

                    "I can even give you the A you deserve. Does that sound good? Perfect.",

                    "Before I can help you, I need you to help me. " +
                    "I see that your programming skills are decent but I don't know if it's good enough yet.",

                    "I need you to prove me to me it's good. " +
                    "If it's good, then I need you to take the fall...err...I mean help me hack the school grading system.",

                    "Do you know anything about functions? I know you've seen len() and range() before. I know all that you do...",

                    "We're going to have you write your own custom function. It's straight forward. " +
                    "You just need to use the keyword 'def' followed by function name and parameters if there are any.",

                    "For example if 'def func_name():' defines a function without any parameters.",

                    "'def func_name_2(param_1):' defines a function with a single parameter. " +
                    "'def func_name_3(param_1, param_2):' defines a function with two parameters.",

                    "Functions can also return a value using the 'return' keyword. Say '    return 5' will return the integer 5. " +
                    "Then you can call the function 'x = func_name()'. 'x' will now be equal to 5.",

                    "Look at your instruction notes for more details on the format of functions.",

                    this.TaskInstructions

                },
                successMessage = "You might just be good enough at programming to be framed for...err...I mean help me on my mission.",
                sprite = NpcNames.HACKER_SPRITE_EVIL
            };
        }

        public override string TaskInstructions
        {
            get
            {
                return $"For this task, you have to write 3 functions. " +
                    $"1st function should be called '{BasicMultiplicationFunctionName}' and will print the multiplication of {MultipleA} and {MultipleB}. " +
                    $"2nd function should be called '{ArgMultiplicationFunctionName}', will have 2 parameters, and will print the multiplication of the 2 parameters. " +
                    $"3rd function should be called '{ArgReturnMultiplicationFunctionName}', will have 2 parameters, and will return the multiplication of the 2 parameters. " +
                    $"Call all 3 functions. For {ArgMultiplicationFunctionName}() pass in {MultipleA} and {MultipleB}. " +
                    $"For {ArgReturnMultiplicationFunctionName}() pass in {MultipleA} and {MultipleB} and print the returned value.";
            }
        }

        private string MultipleA
        {
            get
            {
                return "5";
            }
        }

        private string MultipleB
        {
            get
            {
                return "8";
            }
        }

        private string BasicMultiplicationFunctionName
        {
            get
            {
                return "basic_mult";
            }
        }

        private string ArgMultiplicationFunctionName
        {
            get
            {
                return "arg_mult";
            }
        }

        private string ArgReturnMultiplicationFunctionName
        {
            get
            {
                return "arg_return_mult";
            }
        }

        public override string StartCode
        {
            get
            {
                return FunctionBasicsStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                return $"40{Environment.NewLine}40{Environment.NewLine}40";
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

            // Do we multiply?
            if (!StringUtils.HasStarOperator(codeLines))
            {
                hint = $"Your code doesn't use the multiplication (*) operator. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we use correct def format?
            if (!StringUtils.HasFunctionDefinition(codeLines))
            {
                hint = $"Your code doesn't format and use the 'def' correctly. You need the keywords 'def', parentheses and the colon (:). For example 'def func_name():'. {NpcNames.HACKER_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Check function names.
            string[] functionNames = new string[] { BasicMultiplicationFunctionName, ArgMultiplicationFunctionName, ArgReturnMultiplicationFunctionName };
            foreach (string functionName in functionNames)
            {
                if (!StringUtils.HasCodeString(codeLines, functionName))
                {
                    hint = $"Your code is missing the function {functionName}. Make sure the spelling is exactly how I said it. {NpcNames.HACKER_HINT_PREFIX}{TaskInstructions}";
                    return false;
                }
            }

            // Check to see if the multiples are used.
            bool hasMultiples = StringUtils.HasCodeString(codeLines, MultipleA) && StringUtils.HasCodeString(codeLines, MultipleB);
            if (!hasMultiples)
            {
                hint = $"Your code is missing the integers {MultipleA} or {MultipleB} (or both). {NpcNames.HACKER_HINT_PREFIX}{TaskInstructions}";
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
