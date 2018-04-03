using Assets.Tutorials;
using System;

namespace Assets.ProblemSets
{
    public class IntegerProblem : NumberVariableProblem
    {
        public static Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Now we're looking at integers. Integers are whole numbers like 1, 2, -5, etc.",
                    "The assignment says that we to change the variable 'number' to 8.",
                    "But bro, it also says we got to do it before printing the variable."
                },
                successMessage = "That seems right. You're so smart bro!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        public override string StartCode
        {
            get
            {
                return $"number = 7{Environment.NewLine}print number";
            }
        }

        public override string Answer
        {
            get
            {
                return "8";
            }
        }
        
        protected override string SpecificInstruction
        {
            get
            {
                return "The variable \"x\" has a value of 7. Change it to the value to 8 and print the variable \"x\".";
            }
        }

        protected override string SpecificHint
        {
            get
            {
                return $"Trying changing the number variable from 7 to {this.Answer}.";
            }
        }
    }
}
