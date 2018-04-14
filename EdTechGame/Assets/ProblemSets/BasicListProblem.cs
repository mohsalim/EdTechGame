using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class BasicListProblem : ListProblem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.PROFESSOR,
                sentences = new string[]
                {
                    "We have time to learn lists then!",
                    "Lists are any number of items put together. You can have an empty list too with nothing in it.",
                    "You can access any item of the list using an index. The index is the position of the item in the list. " +
                    "Python is 0-based meanig the first item has index 0, the second item has index 1, third item has inde 2, and so on.",
                    "You can get the size or length of the list by using the len() function. " +
                    "For example, len([1,4]) should give you 2 because there are 2 items in the list.",
                    "You can mix types in a list. For example, ['hi', 7.2, 3, True] is a valid list.",
                    "This might be over your head, so feel free to drop out of my class.",
                    this.TaskInstructions

                },
                successMessage = "You must have gotten help from someone... " +
                "For the next task, you must work alone AND you must print out your code and submit it to me! " +
                "I am old school like that...and it's easier on my eyes...",
                sprite = NpcNames.PROFESSOR_SPRITE_HAPPY
            };
        }

        public override string TaskInstructions
        {
            get
            {
                return "For this task, create a list with 4 items: 'first', 5000, False, 5000. " +
                    "It must be in that order. Print the list. Then print the 3rd item in the list using indexing. " +
                    "Finally print length of the list using the len() function.";
            }
        }

        public override string StartCode
        {
            get
            {
                return BasicStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                return $"['first', 5000, False, 5000]{Environment.NewLine}False{Environment.NewLine}4";
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

            // Do we use len() function?
            if (!StringUtils.HasLengthFunction(codeLines))
            {
                hint = $"Your code doesn't use the len() function. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Do we index?
            if (!StringUtils.HasSquareBrackets(codeLines))
            {
                hint = $"Your code doesn't index or you don't create the list. You need to use square brackets [] to do either. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
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
            if (codeOutputLines.Length != 3)
            {
                hint = $"Your code prints too {StringUtils.GetFewOrMany(3, codeOutputLines.Length)} things. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Make sure list has exactly 4 items: ['first', 5000, False, 5000].
            string[] listParts = codeOutputLines[0].Split(',');
            if (listParts.Length != 4)
            {
                hint = $"Your list should have 4 items. It currently has {listParts.Length} items. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Check to see which is incorrect from the list.
            string[] listAnswers = { "['first'", " 5000", " False", " 5000]" };
            for (int i = 0; i < listParts.Length; i++)
            {
                if (listParts[i] != listAnswers[i])
                {
                    hint = $"Your list's {StringUtils.GetNthText(i + 1)} item is wrong. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                    return false;
                }
            }

            // Is the user not getting the third item?
            if (codeOutputLines[1] != "False")
            {
                hint = $"Your must index the 3rd item in the list. Remember that Python is 0-based. This means that first item has 0 index, second item has 1 index, and so on. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Make sure the correct list length is printed.
            if (codeOutputLines[2] != "4")
            {
                hint = $"You need to print the length of the list using the len() function. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
                return false;
            }

            // Some unknown use case occurred.
            hint = $"Your output seems incorrect. {NpcNames.PROFESSOR_HINT_PREFIX}{this.TaskInstructions}";
            return false;
        }
    }
}
