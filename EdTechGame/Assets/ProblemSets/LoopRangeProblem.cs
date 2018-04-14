using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class LoopRangeProblem : LoopProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.TA,
                sentences = new string[]
                {
                    $"(You head straight to the TA's office after class) " +
                    $"Ugh, another lazy student who just wants extra credit. Why don't you do your homework and maybe you'd pass class normaly?",
                    "Bleh...I'm too busy working on my homework to help you. I don't have time to come up with a make up task for you.",
                    "(The TA yawns violently) Even if I did have time, I'm too dead tired to help...",
                    "(You offer to help her on her homework) You think you can help me on this? You're joking right?",
                    "Fine. Help me with this for as your extra credit assignment. Are you famaliar with for loops that use the range() function?",

                    "You can iterate over a range of numbers. Usually you have to make the list yourself like [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]." +
                    "But what if you had to go to 100 or 1000? You want to use the range() function. It'll create a list for you. Say you want 0 to 100. " +
                    "You can do range(101). Notice the number is 1 more than the last number!",

                    this.TaskInstructions

                },
                successMessage = "Wow. That was amazing. I'll change your F to a C. Congrats....and thanks. " +
                "Now if you don't mind, I'm going to catch up on sleep in my office. (You leave the office)",
                sprite = NpcNames.TA_SPRITE_UPSET
            };
        }

        protected override string TaskInstructions
        {
            get
            {
                return "For this task, generate a list of numbers using the range() function that go 0 to 8 (inclusively). " +
                    "Iterate over the list using a for loop. " +
                    "Inside the loop divide that iterated item from the list by 2.0 and print it out.";
            }
        }

        public override string StartCode
        {
            get
            {
                return LoopRangeStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                return $"0.0{Environment.NewLine}" +
                    $"0.5{Environment.NewLine}" +
                    $"1.0{Environment.NewLine}" +
                    $"1.5{Environment.NewLine}" +
                    $"2.0{Environment.NewLine}" +
                    $"2.5{Environment.NewLine}" +
                    $"3.0{Environment.NewLine}" +
                    $"3.5{Environment.NewLine}" +
                    $"4.0";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            // Empty/null case.
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"Your code isn't printing anything. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Trim and split code into code lines.
            code = code.Trim();
            string[] codeLines = StringUtils.SplitByLines(code);

            // Do we divide?
            bool hasDivision = StringUtils.HasCodeString(codeLines, "/");
            if (!hasDivision)
            {
                hint = $"Your code doesn't use the division (/) operator. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we square brackets for array?
            bool generateList = StringUtils.HasCodeString(codeLines, "range(") && StringUtils.HasCodeString(codeLines, ")");
            if (!generateList)
            {
                hint = $"Your code doesn't seem to generate the list. You need to use the range() function to generate the list. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we use the proper for loop.
            bool usesProperForLoop = StringUtils.HasCodeString(codeLines, "for") && StringUtils.HasCodeString(codeLines, "in") && StringUtils.HasCodeString(codeLines, ":");
            if (!usesProperForLoop)
            {
                hint = $"Your code doesn't format and use the for loop correctly. You need the keywords 'for' and 'in' as well as the colon (:). For example 'for x in xs:'. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
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
                hint = $"Your output prints too {StringUtils.GetFewOrMany(answerLines.Length, codeOutputLines.Length)} things. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Find out if a specific output is wrong by line.
            for (int i = 0; i < answerLines.Length; i++)
            {
                if (codeOutputLines[i] != answerLines[i])
                {
                    hint = $"Your output prints the wrong {StringUtils.GetNthText(i + 1)} item. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
                    return false;
                }
            }

            // Some other unknown error.
            hint = $"Your output seems wrong. {NpcNames.TA_HINT_PREFIX}{this.TaskInstructions}";
            return false;
        }
    }
}
