using System;

namespace Assets.ProblemSets
{
    public class FunctionTest : FunctionProblem
    {
        public override Dialogue GetDialogue()
        {
            throw new NotImplementedException();
        }

        public override string TaskInstructions
        {
            get
            {
                return $"For this task, create a function called {FunctionName} that will be given a single integer parameter. " +
                    $"Create a variable with the value 1." +
                    $"You will iterate from 0 and up to the parameter (exclusively!). " +
                    $"As you iterate, you set the variable to the item in the list with the variable itself (ranged product)" +
                    $"After the loop, print your variable. " +
                    $"Call your function with arguments: {ParamA}, {ParamB}, and {ParamC}.";
            }
        }

        private string FunctionName
        {
            get
            {
                return "product_range";
            }
        }

        private string ParamA
        {
            get
            {
                return "5";
            }
        }

        private string ParamB
        {
            get
            {
                return "9";
            }
        }

        private string ParamC
        {
            get
            {
                return "100";
            }
        }

        public override string StartCode
        {
            get
            {
                return FunctionTestStartCode;
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
