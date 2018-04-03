using System;

namespace Assets.ProblemSets
{
    public class IntegerProblem : NumberVariableProblem
    {
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
