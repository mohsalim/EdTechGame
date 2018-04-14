using Assets.Tutorials;
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
                    $"(Next day in class)" +
                    $"I've finished grading everyone's list assignment from yesterday. Most of you got an F as expected.",

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
                    $"50{Environment.NewLine}";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            throw new NotImplementedException();
        }
    }
}
