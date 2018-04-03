using Assets.Tutorials;
using System;

namespace Assets.ProblemSets
{
    public class FloatingPointProblem : NumberVariableProblem
    {
        public static Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Next up is floating points. These are numbers with decimals like 1.2, 4.0, -0.2, etc.",
                    "You can write floating points as is or you can convert integers to floats using functions.",
                    "Functions can be confusing at first bro, but all they do is take in variables and return a new variable.",
                    $"You can see from {NpcNames.PROFESSOR}'s example how to do both. Prof wants us to print out a variable with 8.2."
                },
                successMessage = "You're catching on. You deserve a brophy!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        public override string StartCode
        {
            get
            {
                return $"myFloat = 7.0{Environment.NewLine}print myFloat{Environment.NewLine}myOtherFloat=float(8){Environment.NewLine}print myOtherFloat";
            }
        }

        public override string Answer
        {
            get
            {
                return "8.2";
            }
        }

        protected override string SpecificInstruction
        {
            get
            {
                return $"The variable \"myFloat\" has a value of 7.0. " +
                    $"The variable \"myOtherFloat\" has the value 8.0 by taking the integer 8 and converting it to 8.0 with the \"float()\" method. " +
                    $"Print just a variable with the value {this.Answer}. Nothing else should be printed.";
            }
        }

        protected override string SpecificHint
        {
            get
            {
                return $"Trying changing one of the number variables to {this.Answer}.";
            }
        }
    }
}
