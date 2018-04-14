using System;

namespace Assets.ProblemSets
{
    public class LoopRangeProblem : LoopProblem
    {
        public override Dialogue GetDialogue()
        {
            throw new NotImplementedException();
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
