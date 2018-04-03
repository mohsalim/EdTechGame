using System;

namespace Assets.ProblemSets
{
    public class FloatingPointProblem : NumberVariableProblem
    {
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
                return $"The variable \"myFloat\" has a value of 7.0. The variable \"myOtherFloat\" has the value 8.0 by taking the integer 8 and converting it to 8.0 with the \"float()\" method. Print just a variable with the value ${this.Answer}. Nothing else should be printed.";
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
