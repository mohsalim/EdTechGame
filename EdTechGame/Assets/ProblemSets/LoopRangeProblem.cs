using Assets.Tutorials;
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
                    $"(You head straight to the TA's office after class)" +
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            throw new NotImplementedException();
        }
    }
}
