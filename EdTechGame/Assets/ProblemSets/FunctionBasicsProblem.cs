using System;

namespace Assets.ProblemSets
{
    public class FunctionBasicsProblem : FunctionProblem
    {
        public override Dialogue GetDialogue()
        {
            throw new NotImplementedException();
        }

        public override string TaskInstructions
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
                return FunctionBasicsStartCode;
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
