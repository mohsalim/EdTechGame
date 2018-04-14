using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class LoopBasicsProblem : LoopProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.PROFESSOR,
                sentences = new string[]
                {
                    $"(Next day in class) I've finished grading everyone's list assignment from yesterday. Most of you got an F as expected.",

                    "(Prof. is passing back assignments one at a time)",

                    "(Prof. passes by Broseph) Good job Broseph! You got an A. I'm impressed.",

                    "(Prof. comes by you) You got an F. You really should visit the TA after class for make up these points..." +
                    "or just drop out. Whichever is easier for you. Maybe even work with Broseph to get better.",

                    "Today we are going to learn for loops. " +
                    "For loops can iterate over a list easily. This means you can go through each item in the list with just 2 lines of code.",

                    "Additionally, for loops are a way to repeat line(s) of code without have to copy and paste that same line(s) of code.",

                    "For loops have two keywords 'for' and 'in'. " +
                    "Sometimes this is referred to as a 'for each' loop. You want to write it as so 'for item in list:'. " +
                    "Then on the next line, write your code. Remember to tab over the code so its in the for loop's scope.",

                    this.TaskInstructions

                },
                successMessage = "How did you get that right? It doesn't matter. " +
                "You still are failing the class because of F on the lists assignment. " +
                $"If you want to make up the grade, you can visit {NpcNames.TA}, but I doubt you'll be able to finish her extra credit work.",
                sprite = NpcNames.PROFESSOR_SPRITE_HAPPY
            };   
        }

        protected override string TaskInstructions
        {
            get
            {
                return "For this task, create a list of numbers: 0, 1, 2, 3, 4, 5. " +
                    "Iterate over the list using a for loop. " +
                    "Inside the loop multiply that iterated item from the list by 10 and print it out.";
            }
        }

        public override string StartCode
        {
            get
            {
                return LoopBasicsStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                return $"0{Environment.NewLine}" +
                    $"10{Environment.NewLine}" +
                    $"20{Environment.NewLine}" +
                    $"30{Environment.NewLine}" +
                    $"40{Environment.NewLine}" +
                    $"50";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            // Empty/null case.
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"Your code isn't printing anything. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Trim and split code into code lines.
            code = code.Trim();
            string[] codeLines = StringUtils.SplitByLines(code);

            // Do we multiply?
            if (!StringUtils.HasStarOperator(codeLines))
            {
                hint = $"Your code doesn't use the multiplication (*) operator. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we square brackets for list?
            if (!StringUtils.HasSquareBrackets(codeLines))
            {
                hint = $"Your code doesn't seem to create the list. You need to use square brackets [] when creating a list. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we use the proper for loop.
            if (!StringUtils.HasForLoop(codeLines))
            {
                hint = $"Your code doesn't format and use the for loop correctly. You need the keywords 'for' and 'in' as well as the colon (:). For example 'for x in xs:'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Is the answer correct?
            codeOutput = codeOutput.Trim();
            if (codeOutput == Answer)
            {
                hint = "";
                return true;
            }

            // Make sure it's 6 lines of code.
            string[] codeOutputLines = StringUtils.SplitByLines(codeOutput);
            string[] answerLines = StringUtils.SplitByLines(Answer);
            if (codeOutputLines.Length != answerLines.Length)
            {
                hint = $"Your output prints too {StringUtils.GetFewOrMany(answerLines.Length, codeOutputLines.Length)} things. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Find out if a specific output is wrong by line.
            for (int i = 0; i < answerLines.Length; i++)
            {
                if (codeOutputLines[i] != answerLines[i])
                {
                    hint = $"Your output prints the wrong {StringUtils.GetNthText(i + 1)} item. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                    return false;
                }
            }

            // Some other unknown error.
            hint = $"Your output seems wrong. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
            return false;
        }
    }
}
